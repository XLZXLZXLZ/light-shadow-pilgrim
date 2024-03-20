using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{    
    [SerializeField] private Transform panelRoot;//对应的Canvas

    [SerializeField] private Image[] panelLayers;//UI面板的层级

    [SerializeField] private Image uiMask;//UI的遮罩，当这个东西启用时无法进行任何UI操作

    [SerializeField] private PanelContainer panelContainer;//各种UI面板预制体的引用

    protected override bool IsDontDestroyOnLoad => true;

    private Dictionary<Type, PanelBase> panelDic = new();       //存有的UI面板预制体的引用
    private Dictionary<Type, PanelBase> panelOnShowing = new(); //正在显示中的UI面板
    private Dictionary<int, bool> isHavePanelShowLayer = new(); //对应层级是否有UI面板正在显示
    public bool isCanOperateUI;//是否可以进行UI操作

    private void Start()
    {
        panelContainer.panels.ForEach(panel => panelDic.Add(panel.GetType(), panel));
        isHavePanelShowLayer = new ()
        {
            {0,false},
            {1,false},
            {2,false},
            {3,false},
            {4,false},
        };

        isCanOperateUI = true;
        uiMask.enabled = false;
    }
    
    public T ShowPanel<T>() where T : PanelBase
    {
        return ShowPanel(typeof(T)) as T;
    }

    public PanelBase ShowPanel(Type panelType)
    {
        if (!isCanOperateUI) return null;
        if (!typeof(PanelBase).IsAssignableFrom(panelType) ||
            !panelDic.ContainsKey(panelType)) return null;
        
        if (panelOnShowing.ContainsKey(panelType))
        {
            panelOnShowing[panelType].OnShowingAndCall();
            return panelOnShowing[panelType];
        }
        
        PanelBase relevantPanel = panelDic[panelType];
        int relevantPanelSortingLayer = relevantPanel.panelSortingLayer;
        if (isHavePanelShowLayer[relevantPanel.panelSortingLayer]) return null;
        
        PanelBase panel = PoolManager.Instance.GetGameObject(relevantPanel,panelLayers[relevantPanelSortingLayer].transform);
        panel.OnShow();
        
        panelOnShowing.Add(panelType,panel);
        isHavePanelShowLayer[relevantPanelSortingLayer] = true;
        panelLayers[relevantPanelSortingLayer].raycastTarget = true;

        return panel;
    }
    
    public void HidePanel<T>() where T : PanelBase
    {
        HidePanel(typeof(T));
    }

    public void HidePanel(Type panelType)
    {
        if (!isCanOperateUI) return;
        if (!panelOnShowing.ContainsKey(panelType) ||
            !typeof(PanelBase).IsAssignableFrom(panelType)) return;
        if (panelOnShowing[panelType].isHiding)
        {
            panelOnShowing[panelType].OnHiding();
            return;
        }

        PanelBase relevantPanel = panelOnShowing[panelType];
        int relevantPanelSortingLayer = relevantPanel.panelSortingLayer;
        
        relevantPanel.OnHide();

        if (relevantPanel.isHideDirectly)
        {
            PoolManager.Instance.PushGameObject(relevantPanel.gameObject);
            panelOnShowing.Remove(panelType);
            isHavePanelShowLayer[relevantPanel.panelSortingLayer] = false;
            panelLayers[relevantPanelSortingLayer].raycastTarget = false;
        }
    }

    public void ClearPanelCache<T>() where T : PanelBase
    {
        ClearPanelCache(typeof(T));
    }
    
    public void ClearPanelCache(Type panelType)
    {
        if (!isCanOperateUI) return;
        if (!panelOnShowing.ContainsKey(panelType) || 
            !typeof(PanelBase).IsAssignableFrom(panelType)) return;
        Debug.Log("ClearPanelCache");
        PanelBase relevantPanel = panelOnShowing[panelType];
        int relevantPanelSortingLayer = relevantPanel.panelSortingLayer;
        
        PoolManager.Instance.PushGameObject(relevantPanel.gameObject);
        panelOnShowing.Remove(panelType);
        isHavePanelShowLayer[relevantPanel.panelSortingLayer] = false;
        panelLayers[relevantPanelSortingLayer].raycastTarget = false;
    }
    
    public T GetPanelOnShowing<T>() where T : PanelBase
    {
        return panelOnShowing.ContainsKey(typeof(T)) ? panelOnShowing[typeof(T)] as T : null;
    }
    
    // public async UniTask LoadSceneAsync(string sceneName,Action onFadeIn = null, Action onLoadCompeleted = null)
    public void LoadSceneAsync(string sceneName,Action onFadeIn = null, Action onFadeOut = null)
    {
        SetMask(true);

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

        SetMask(false);
    }

    public void SetMask(bool b)
    {
        uiMask.enabled = b;
    }
}