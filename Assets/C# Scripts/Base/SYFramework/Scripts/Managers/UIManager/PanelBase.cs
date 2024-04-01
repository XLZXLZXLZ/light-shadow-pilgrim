using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PanelDisplayState
{
    Hide,
    InShowAnim,
    Idle,
    InHideAnim,
}
public abstract class PanelBase : MonoBehaviour
{
    // public bool IsCanOperatePanel { get; set; }
    
    public StageEvent init = new();
    public StageEvent show = new();
    public StageEvent hide = new();
    public StageEvent saveSelfCache = new();
    public StageEvent clearSelfCache = new();
    public PanelDisplayState PanelDisplayState { get; private set; } = PanelDisplayState.Hide;
    
    protected virtual void Start()
    {
        Init();
    }

    #region Screen Params

    public static Vector2 ScreenCenter => new(Screen.width / 2, Screen.height / 2);
    public static Vector2 ScreenUpCenter => ScreenCenter + Vector2.up * Screen.height;
    public static Vector2 ScreenDownCenter => ScreenCenter + Vector2.down * Screen.height;
    public static Vector2 ScreenLeftCenter => ScreenCenter + Vector2.left * Screen.width;
    public static Vector2 ScreenRightCenter => ScreenCenter + Vector2.right * Screen.width;

    #endregion

    #region AbstractFunc

    /// <summary>
    /// 面板初始化，只会执行一次
    /// </summary>
    protected abstract void OnInit();

    /// <summary>
    /// 面板显示时执行的逻辑
    /// </summary>
    protected abstract void OnShow();
    
    /// <summary>
    /// 面板隐藏时执行的逻辑，不要在其他地方调用这个函数，而是通过UIManager去管理
    /// </summary>
    protected abstract void OnHide();

    protected abstract void ShowAnim();
    protected abstract void HideAnim();

    #endregion

    #region LocalFunc

    public virtual void Init()
    {
        init.StartStageEvent();
        OnInit();
        init.FinishStageEvent();
    }

    public void Show(bool isShowAndHideDirectly)
    {
        show.StartStageEvent();
        OnShow();
        ShowAnim();
        PanelDisplayState = PanelDisplayState.InShowAnim;
        if (isShowAndHideDirectly)
            ShowAnimFinished();
    }
    
    protected void ShowAnimFinished()
    {
        PanelDisplayState = PanelDisplayState.Idle;
        show.FinishStageEvent();
    }

    public void Hide(bool isShowAndHideDirectly)
    {
        hide.StartStageEvent();
        OnHide();
        HideAnim();
        PanelDisplayState = PanelDisplayState.InHideAnim;
        if (isShowAndHideDirectly)
            // HideAnimFinished();
        {
            PanelDisplayState = PanelDisplayState.Hide;
            hide.FinishStageEvent();
        }
    }
    
    protected void HideAnimFinished()
    {
        PanelDisplayState = PanelDisplayState.Hide;
        hide.FinishStageEvent();
        SaveSelfCache();
    }

    // protected void HideAnimFinishedAndSaveCache()
    // {
    //     HideAnimFinished();
    //     SaveSelfCache();
    // }

    public virtual void OnSaveCacheStarted()
    {
        saveSelfCache.StartStageEvent();
    }

    public virtual void OnSaveCacheFinished()
    {
        saveSelfCache.FinishStageEvent();
    }

    public virtual void OnClearCacheStarted()
    {
        clearSelfCache.StartStageEvent();
    }

    public virtual void OnClearCacheFinished()
    {
        clearSelfCache.FinishStageEvent();
    }

    #endregion

    #region Extensions

    /// <summary>
    /// 显示自己这个面板
    /// </summary>
    protected void ShowSelf()
    {
        UIManager.Instance.ShowPanel(this.GetType());
    }
    
    /// <summary>
    /// 隐藏自己这个面板，推荐隐藏自己调用时调用
    /// </summary>
    protected void HideSelf()
    {
        UIManager.Instance.HidePanel(this.GetType());
    }

    /// <summary>
    /// 保存UIManager对这个面板的缓存，应该在UI面板的结束动画那对它调用，如果勾选了IsHideDirectly，则自动缓存
    /// </summary>
    protected void SaveSelfCache()
    {
        UIManager.Instance.SavePanelCache(this.GetType());
    }

    /// <summary>
    /// 清理UIManager对这个面板的缓存
    /// </summary>
    protected void ClearSelfCache()
    {
        UIManager.Instance.ClearPanelCache(this.GetType());
    }

    #endregion
}

// protected override void OnInit()
// {
//     base.Init();
// }
//
// public override void OnShow()
// {
//     base.OnShow();
// }
//
// public override void OnHide()
// {
//     base.OnHide();
// }
