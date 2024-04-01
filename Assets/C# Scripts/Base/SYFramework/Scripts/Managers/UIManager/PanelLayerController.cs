using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelLayerController : MonoBehaviour
{
    // PanelLayerController本地相关变量
    public Image mask;
    public int ControllerLayerIndex { get; private set; }
    public bool IsCanOperateThisLayer { get; set; } = true;
    
    // PanelRuntimeInfo相关变量
    public PanelRuntimeInfo CurrentPanelRuntimeInfo { get; private set; }
    public PanelCacheInfo CurrentPanelCacheInfo => CurrentPanelRuntimeInfo.CacheInfo;
    public PanelConfigInfo CurrentPanelConfigInfo => CurrentPanelCacheInfo.ConfigInfo;
    public bool IsShowing => CurrentPanelRuntimeInfo != null;
    public int PanelLayerIndex => CurrentPanelConfigInfo.layerIndex;

    // UIManager进行ShowPanel和HidePanel操作仅仅是调用了PanelLayerController中的函数，对于是否成功一无所知，所以需要整一个回调
    public Action<PanelLayerController> onShowPanel;
    public Action<PanelLayerController> onHidePanel;

    public void Init(int layerIndex)
    {
        this.ControllerLayerIndex = layerIndex;
    }

    /// <summary>
    /// ShowPanel检测流程：
    /// 1. null检查
    /// 2. 如果当前有Panel正在显示，则不能继续显示
    /// 3. 如果当前的Layer不能操作或者传入的Panel不能操作，则返回
    /// </summary>
    public void ShowPanel(PanelRuntimeInfo panelRuntimeInfo)
    {
        if (panelRuntimeInfo == null)
        {
#if UNITY_EDITOR
            SYLog.LogError($"PanelLayerController：尝试显示一个空的Panel");
#endif
            return;
        }
        
        if (IsShowing)
        {
#if UNITY_EDITOR
            SYLog.LogWarning($"PanelLayerController：{PanelLayerIndex}上已经有Panel在显示");
#endif
            return;
        }
        
        if (!IsCanOperateThisLayer || 
            !panelRuntimeInfo.IsCanControl || 
            panelRuntimeInfo.RuntimeState == PanelRuntimeState.ShowingOrHiding) 
            return;
        
        CurrentPanelRuntimeInfo = panelRuntimeInfo;
        if (!panelRuntimeInfo.CacheInfo.IsCache)
        {
            panelRuntimeInfo.CacheInfo.LoadCacheAsync(true, mask.transform)
                .Completed += _ =>
            {
                if (CurrentPanelCacheInfo.PanelCache == null)
#if UNITY_EDITOR
                {
                    SYLog.LogError($"PanelLayerController：将要生成的Panel为空，是否加载的路径出错？");
                    return;
                }
#endif
                CurrentPanelCacheInfo.PanelCache.gameObject.SetActive(true);
                CurrentPanelCacheInfo.PanelCache.Show(CurrentPanelConfigInfo.isHideDirectly);
            };
        }
        else
        {
            panelRuntimeInfo.CacheInfo.PanelCache.gameObject.SetActive(true);
            CurrentPanelCacheInfo.PanelCache.Show(CurrentPanelConfigInfo.isHideDirectly);
        }
        
        onShowPanel?.Invoke(this);
        mask.raycastTarget = true;
    }

    /// <summary>
    /// HidePanel检测流程：
    /// 1. 如果虚空隐藏，则返回
    /// 2. 如果不能操作这个Panel或者这个Panel当前不能被操作，则返回
    /// </summary>
    public void HidePanel()
    {
        if (!IsShowing || CurrentPanelCacheInfo == null)
        {
#if UNITY_EDITOR
            SYLog.LogWarning($"PanelLayerController：正在尝试卸载在{PanelLayerIndex}上的空Panel");
#endif
            return;
        }
        
        if (!IsCanOperateThisLayer || 
            !CurrentPanelRuntimeInfo.IsCanControl)
            return;
        
        CurrentPanelCacheInfo.PanelCache.Hide(CurrentPanelCacheInfo.ConfigInfo.isHideDirectly);
        if(CurrentPanelCacheInfo.ConfigInfo.isHideDirectly)
            CurrentPanelCacheInfo.SaveCache();
        CurrentPanelRuntimeInfo = null;
        
        onHidePanel?.Invoke(this);
        mask.raycastTarget = false;
    }
}

