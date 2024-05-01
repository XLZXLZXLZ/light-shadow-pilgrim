using System;
using System.Collections.Generic;
using UnityEngine;

public enum PanelRuntimeState
{
    NoCache,
    Idle,
    ShowingOrHiding,
    CachedAndHide,
}

public class PanelRuntimeInfo
{
    public PanelCacheInfo CacheInfo { get; private set; }
    public bool IsCanControl => IsCanControlInternal;
    public bool IsCanControlByKeyCode => IsCanControlByKeyCodeInternal && IsCanControl;
    private bool IsCanControlInternal { get; set; } = true;
    private bool IsCanControlByKeyCodeInternal { get; set; } = false;
    public PanelConfigInfo ConfigInfo => CacheInfo.ConfigInfo;
    public PanelBase PanelCache => CacheInfo.PanelCache;
    public int LayerIndex => ConfigInfo.layerIndex;
    public bool IsControlledByLayer => ConfigInfo.isControlledByLayer;
    
    public PanelRuntimeState RuntimeState
    {
        get
        {
            if (!CacheInfo.IsCache)
                return PanelRuntimeState.NoCache;
            if (CacheInfo.PanelCache.PanelDisplayState == PanelDisplayState.Idle)
                return PanelRuntimeState.Idle;
            if (CacheInfo.PanelCache.PanelDisplayState == PanelDisplayState.Hide)
                return PanelRuntimeState.CachedAndHide;
            return PanelRuntimeState.ShowingOrHiding;
        }
    }

    public PanelRuntimeInfo(PanelCacheInfo cacheInfo)
    {
        this.CacheInfo = cacheInfo;
    }

    public void SetIsCanControl(bool isCanControl)
    {
        IsCanControlInternal = isCanControl;
    }

    public void SetIsCanControlByKeyCode(bool isCanControlByKeyCode)
    {
        IsCanControlByKeyCodeInternal = isCanControlByKeyCode;
    }
}

