using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : ManagerBase<UIManager>
{    
    /// <summary>
    /// PanelLayerController列表，用于管控各种Panel行为
    /// </summary>
    [SerializeField] private List<PanelLayerController> panelLayerControllers = new();
    
    /// <summary>
    /// UI的遮罩，当这个东西启用时无法进行任何UI操作
    /// </summary>
    [SerializeField] private Image uiMask;
    
    /// <summary>
    /// Panel的原生信息
    /// </summary>
    private List<PanelConfigInfo> PanelInfoDic => UISettings.panelInfos;
    
    /// <summary>
    /// Panel以层级分类的信息
    /// </summary>
    private Dictionary<int, List<PanelRuntimeInfo>> panelInfoList = new();

    /// <summary>
    /// 存储Panel缓存信息的数据
    /// </summary>
    public Dictionary<Type, PanelRuntimeInfo> PanelRuntimeInfoDic { get; private set; } = new();
    
    /// <summary>
    /// 当前正在显示的UI栈结构
    /// </summary>
    public Stack<PanelLayerController> PanelOnShowing { get; private set; } = new();

    // public Dictionary<KeyCode, List<Type>> PanelBindingKeyCode { get; private set; } = new();
    public int TopPanelLayerIndex => PanelOnShowing.Count == 0 ? -1 : PanelOnShowing.Peek().ControllerLayerIndex;
    public int PanelLayerCount => panelLayerControllers.Count;
    
    private UISettings uiSettings;
    private UISettings UISettings
    {
        get
        {
            if (uiSettings == null)
                uiSettings = Root.GetRuntimeSettings().uiSettings;
            return uiSettings;
        }
    }
    
    private bool isCanOperateUI = true; // 是否可以进行UI操作
    public bool IsCanOperateUI
    {
        get => isCanOperateUI;
        set
        {
            isCanOperateUI = value;
            uiMask.enabled = !isCanOperateUI;
        } 
    } 

    public override void Awake()
    {
        base.Awake();
        uiMask.enabled = false;

        InitPanelLayerController();
        InitPanelRuntimeDic();
    }

    private void Update()
    {
        BindingKeyCodeCheck();
    }

    #region Init

    private void InitPanelLayerController()
    {
        foreach (var panelInfo in PanelInfoDic)
        {
            PanelRuntimeInfo runtimeInfo = new PanelRuntimeInfo(new PanelCacheInfo(panelInfo));
            PanelRuntimeInfoDic.Add(panelInfo.panelType,runtimeInfo);

            int panelLayerIndex = panelInfo.layerIndex;
            if (panelInfoList.ContainsKey(panelLayerIndex))
                panelInfoList[panelLayerIndex].Add(runtimeInfo);
            else
                panelInfoList.Add(panelLayerIndex, new(){runtimeInfo});
        }
    }

    private void InitPanelRuntimeDic()
    {
        for (int i = 0; i < panelLayerControllers.Count; i++)
        {
            panelLayerControllers[i].Init(i);
            panelLayerControllers[i].onShowPanel += OnControllerShowPanel;
            panelLayerControllers[i].onHidePanel += OnControllerHidePanel;
        }
    }

    #endregion

    #region Tools

    /// <summary>
    /// 发现IsContainPanelConfigInfo和IsCanContainUpPanel多处一块出现，所以整了一个这个函数
    /// </summary>
    private bool IsCanOperatePanel(Type type, out PanelRuntimeInfo runtimeInfo)
    {
        return IsContainPanelConfigInfo(type,out runtimeInfo) && !IsCanContainUpPanel(runtimeInfo);
    }

    /// <summary>
    /// 检查是否有这个类型的Panel，如果有则out出一个缓存信息
    /// </summary>
    private bool IsContainPanelConfigInfo(Type type, out PanelRuntimeInfo runtimeInfo)
    {
        if (type == null || !PanelRuntimeInfoDic.ContainsKey(type))
        {
#if UNITY_EDITOR
            SYLog.LogError($"UIManager：找不到Key为{type}的缓存信息！");
#endif

            runtimeInfo = null;
            return false;
        }

        runtimeInfo = PanelRuntimeInfoDic[type];
        return true;
    }
    
    /// <summary>
    /// 是否存在上层UI
    /// </summary>
    /// <param name="cacheInfo"></param>
    /// <returns></returns>
    private bool IsCanContainUpPanel(PanelRuntimeInfo runtimeInfo)
    {
        if (PanelOnShowing.Count != 0 &&
            PanelOnShowing.Peek().ControllerLayerIndex > runtimeInfo.LayerIndex)
        {
#if UNITY_EDITOR
            SYLog.LogWarning("UIManager：奶奶滴，存在上层UI时不能操作下层UI！");
#endif
            return true;
        }

        return false;
    }
    
    /// <summary>
    /// 获取对应缓存信息中Panel的父物体
    /// </summary>
    private Transform GetPanelParent(PanelRuntimeInfo runtimeInfo)
    {
        return panelLayerControllers[runtimeInfo.ConfigInfo.layerIndex].transform;
    }

    /// <summary>
    /// 在Panel保存缓存和清除缓存前确保正确关闭（Hide）
    /// </summary>
    private void SaveOrClearPanelCacheCheck(PanelRuntimeInfo runtimeInfo)
    {
        PanelLayerController controller = panelLayerControllers[runtimeInfo.LayerIndex];
        if (controller.CurrentPanelRuntimeInfo != null)
        {
#if UNITY_EDITOR
            SYLog.LogWarning("UIManager：未能在执行Hide方法后保存或清除缓存");
#endif
            controller.HidePanel();
        }
    }
    
    #endregion

    #region PanelControl

    /// <summary>
    /// 获取Panel的当前状态
    /// <br/>***仅推荐PanelBase类中调用***
    /// </summary>
    public PanelRuntimeState GetPanelRuntimeState<T>()
    {
        return GetPanelRuntimeState(typeof(T));
    }
    
    /// <summary>
    /// 获取Panel的当前状态
    /// <br/>***仅推荐PanelBase类中调用***
    /// </summary>
    public PanelRuntimeState GetPanelRuntimeState(Type type)
    {
        if (!IsContainPanelConfigInfo(type, out PanelRuntimeInfo runtimeInfo))
        {
            throw new Exception("别瞎搞，谢谢您QWQ");
        }

        return runtimeInfo.RuntimeState;
    }

    #endregion

    #region CacheControl

    /// <summary>
    /// 异步加载Panel的缓存
    /// </summary>
    private AsyncOperationHandle<GameObject> LoadPanelCacheAsync<T>() where T: PanelBase
    {
        return LoadPanelCacheAsync(typeof(T));
    }
    
    /// <summary>
    /// 异步加载Panel的缓存
    /// </summary>
    private AsyncOperationHandle<GameObject> LoadPanelCacheAsync(Type type)
    {
        if (!IsContainPanelConfigInfo(type, out PanelRuntimeInfo runtimeInfo)) return default;
        return runtimeInfo.CacheInfo.LoadCacheAsync(false,GetPanelParent(runtimeInfo));
    }

    /// <summary>
    /// 存储Panel的缓存（SetActive false，但是不Destroy掉）
    /// 当存在上层UI且该Panel在显示时不能存储Panel的缓存,仅推荐PanelBase类中调用
    /// <br/>***仅推荐PanelBase类中调用***
    /// </summary>
    public void SavePanelCache<T>()where T: PanelBase
    {
        SavePanelCache(typeof(T));
    }

    /// <summary>
    /// 存储Panel的缓存（SetActive false，但是不Destroy掉）
    /// 当存在上层UI且该Panel在显示时不能存储Panel的缓存
    /// <br/>***仅推荐PanelBase类中调用***
    /// </summary>
    public void SavePanelCache(Type type)
    {
        if (!IsCanOperateUI) return;

        // 此时可以操作UI 或者 要保存缓存的UI没在显示时 可以保存UI缓存
        if (IsCanOperatePanel(type, out PanelRuntimeInfo runtimeInfo) ||
            !panelLayerControllers[runtimeInfo.LayerIndex].IsShowing)
        {
            SaveOrClearPanelCacheCheck(runtimeInfo);
            runtimeInfo.CacheInfo.SaveCache();
        }
    }
    
    /// <summary>
    /// 清理Panel的缓存（Destroy掉，且Release掉Handle）
    /// 当存在上层UI且该Panel在显示时不能清理Panel的缓存
    /// <br/>***仅推荐PanelBase类中调用***
    /// </summary>
    public void ClearPanelCache<T>() where T : PanelBase
    {
        ClearPanelCache(typeof(T));
    }
    
    /// <summary>
    /// 清理Panel的缓存（Destroy掉，且Release掉Handle）
    /// 当存在上层UI且该Panel在显示时不能清理Panel的缓存
    /// <br/>***仅推荐PanelBase类中调用***
    /// </summary>
    public void ClearPanelCache(Type type)
    {
        if (!IsCanOperateUI) return;

        // 此时可以操作UI 或者 要清理缓存的UI没在显示时 可以清理UI缓存
        if (IsCanOperatePanel(type, out PanelRuntimeInfo runtimeInfo) ||
            !panelLayerControllers[runtimeInfo.LayerIndex].IsShowing)
        {
            SaveOrClearPanelCacheCheck(runtimeInfo);
            runtimeInfo.CacheInfo.ClearCache();
        }
    }

    #endregion

    #region MainFunc
    // 关于ShowPanel和HidePanel的检测流程说明：
    // 1. 检查是否可以操作UI，如果不能则返回
    // 2. 检查这个Panel当前可不可操作，如果不能则返回
    // 3. 检查这个Panel当前是否在关闭或者打开状态，如果是则返回
    
    /// <summary>
    /// 获取Panel的引用，如果还没加载就自动加载好
    /// </summary>
    public T GetPanel<T>() where T : PanelBase
    {
        return GetPanel(typeof(T)) as T;
    }
    
    /// <summary>
    /// 获取Panel的引用，如果还没加载就自动加载好
    /// </summary>
    public PanelBase GetPanel(Type type)
    {
        if (!IsContainPanelConfigInfo(type, out PanelRuntimeInfo runtimeInfo)) return default;

        if (!runtimeInfo.CacheInfo.IsCache)
            return runtimeInfo.CacheInfo.LoadCache(type,false, GetPanelParent(runtimeInfo));
        
        return runtimeInfo.PanelCache;
    }

    /// <summary>
    /// 异步获取Panel的引用，如果还没加载就自动加载好
    /// </summary>
    public void GetPanelAsync<T>(Action<T> onLoadCompleted = null) where T : PanelBase
    {
        if (!IsContainPanelConfigInfo(typeof(T), out PanelRuntimeInfo runtimeInfo)) return;

        if (!runtimeInfo.CacheInfo.IsCache)
            runtimeInfo.CacheInfo.LoadCacheAsync(false,GetPanelParent(runtimeInfo)).Completed += 
                 _ =>onLoadCompleted?.Invoke(runtimeInfo.PanelCache as T);
        else
            onLoadCompleted?.Invoke(runtimeInfo.PanelCache as T);
    }
    
    /// <summary>
    /// 显示一个Panel，如果还未缓存则自动缓存
    /// </summary>
    public T ShowPanel<T>() where T : PanelBase
    {
        return ShowPanel(typeof(T)) as T;
    }

    /// <summary>
    /// 显示一个Panel，如果还未缓存则自动缓存
    /// </summary>
    public PanelBase ShowPanel(Type type)
    {
        if (!IsCanOperateUI) return null;

        if (!IsCanOperatePanel(type,out PanelRuntimeInfo runtimeInfo)) return null;

        if (runtimeInfo.RuntimeState == PanelRuntimeState.ShowingOrHiding) return null;
        
        PanelLayerController controller = panelLayerControllers[runtimeInfo.ConfigInfo.layerIndex];
        controller.ShowPanel(runtimeInfo);
        
        return runtimeInfo.CacheInfo.PanelCache;
    }
    
    /// <summary>
    /// 隐藏一个Panel，如果isHideDirectly为false需要手动保存或清理缓存
    /// </summary>
    public void HidePanel<T>() where T : PanelBase
    {
        HidePanel(typeof(T));
    }

    /// <summary>
    /// 隐藏一个Panel，如果isHideDirectly为false需要手动保存或清理缓存
    /// </summary>
    public void HidePanel(Type type)
    {
        if (!IsCanOperateUI) return;
        
        if (!IsCanOperatePanel(type,out PanelRuntimeInfo runtimeInfo)) return;
        
        if (runtimeInfo.RuntimeState == PanelRuntimeState.ShowingOrHiding) return;
        
        PanelLayerController controller = panelLayerControllers[runtimeInfo.LayerIndex];
        if (PanelOnShowing.Count == 0 || PanelOnShowing.Peek() != controller)
        {
#if UNITY_EDITOR
            SYLog.LogWarning("UIManager：不应该出现的情况，我的问题QWQ");
            return;
#endif
        }
        
        if (!runtimeInfo.CacheInfo.IsCache || !controller.IsShowing)
        {
#if UNITY_EDITOR
            SYLog.LogWarning("UIManager：尝试隐藏一个没有显示或缓存的Panel！");
#endif
            return;
        }
        
        controller.HidePanel();
    }

    private void OnControllerShowPanel(PanelLayerController controller)
    {
        PanelOnShowing.Push(controller);
    }

    private void OnControllerHidePanel(PanelLayerController controller)
    {
        if (PanelOnShowing.Count == 0 || PanelOnShowing.Peek() != controller)
        {
#if UNITY_EDITOR
            SYLog.LogError("UIManager：这啥情况，这不对吧");
#endif
        }
        PanelOnShowing.Pop();
    }
    
    public void SetPanelCanControlByKeyCode<T>(bool isCanControlByKeyCode) where T : PanelBase
    {
        SetPanelCanControlByKeyCode(typeof(T), isCanControlByKeyCode);
    }

    public void SetPanelCanControlByKeyCode(Type type, bool isCanControlByKeyCode)
    {
        if (!IsContainPanelConfigInfo(type, out PanelRuntimeInfo runtimeInfo))
        {
#if UNITY_EDITOR
            SYLog.LogWarning("UIManager：没有这种类型的Panel！");
#endif
            return;
        }

        runtimeInfo.SetIsCanControlByKeyCode(isCanControlByKeyCode);
    }
    
    #endregion

    #region BindingKeyCode

    private void BindingKeyCodeCheck()
    {
        for (int i = TopPanelLayerIndex; i >= 0; i--)
        {
            if (panelInfoList.TryGetValue(i, out List<PanelRuntimeInfo> runtimeInfos))
            {
                foreach (var runtimeInfo in runtimeInfos)
                {
                    if (Input.GetKeyDown(runtimeInfo.ConfigInfo.hideKeyCode) && runtimeInfo.IsCanControlByKeyCode)
                    {
                        HidePanel(runtimeInfo.ConfigInfo.panelType);
                        return;
                    }
                }
            }
        }
        
        for (int i = TopPanelLayerIndex + 1; i < PanelLayerCount; i++)
        {
            if (panelInfoList.TryGetValue(i, out List<PanelRuntimeInfo> runtimeInfos))
            {
                foreach (var runtimeInfo in runtimeInfos)
                {
                    if (Input.GetKeyDown(runtimeInfo.ConfigInfo.hideKeyCode) && runtimeInfo.IsCanControlByKeyCode)
                    {
                        ShowPanel(runtimeInfo.ConfigInfo.panelType);
                        return;
                    }
                }
            }
        }
        
    }

    #endregion

    #region Others

    // public async UniTask LoadSceneAsync(string sceneName,Action onFadeIn = null, Action onLoadCompeleted = null)
    public void LoadSceneAsync(string sceneName,Action onFadeIn = null, Action onFadeOut = null)
    {
        IsCanOperateUI = false;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        operation.allowSceneActivation = false;
        
        onFadeIn?.Invoke();

        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
                break;
        }

        operation.allowSceneActivation = true;
        // await UniTask.DelayFrame(5);
        onFadeOut?.Invoke();

        IsCanOperateUI = true;
    }

    #endregion
    
    
}