using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PanelBase : MonoBehaviour
{
    //是否正在显示（当isHideDirectly为真时，UI面板在Hiding的过渡阶段时该值也为真）
    public bool isShowing { get; private set; }

    //是否在隐藏阶段（当isHideDirectly为真时，在隐藏面板时不会直接SetActive(false),需要手动调用ClearSelfCache来隐藏面板）
    public bool isHiding { get; private set; }
    public event Action onHideCallback;
    
    //面板是否完全隐藏
    public bool isHideCompeleted => (!isShowing) && (!isHiding);
    
    //面板的sortingLayer（sortingLayer越高，面板显示越靠前）
    public abstract int panelSortingLayer { get;}
    
    //隐藏面板时是否直接SetActive(false)
    public abstract bool isHideDirectly { get; }

    protected virtual void Start()
    {
        Init();
    }

    /// <summary>
    /// 面板初始化，目前只推荐进行给按钮增加监听的操作
    /// </summary>
    protected virtual void Init()
    {
        
    }

    /// <summary>
    /// 面板显示时执行的逻辑
    /// </summary>
    public virtual void OnShow()
    {
        isHiding = false;
        isShowing = true;
    }

    /// <summary>
    /// 如果面板此时仍在显示，但还是想打开面板
    /// </summary>
    public virtual void OnShowingAndCall()
    {
        isHiding = false;
    }

    /// <summary>
    /// 面板隐藏时执行的逻辑
    /// </summary>
    public virtual void OnHide()
    {
        if (isHideDirectly)
        {
            isHiding = true;
            isShowing = false;
            onHideCallback?.Invoke();
        }
    }

    /// <summary>
    /// isHiding为真时，尝试隐藏面板时调用的函数
    /// </summary>
    public virtual void OnHiding()
    {
        
    }

    /// <summary>
    /// 隐藏自己这个面板
    /// </summary>
    protected void HideSelf()
    {
        UIManager.Instance.HidePanel(this.GetType());
    }

    /// <summary>
    /// 清理UIManager对这个面板的缓存
    /// </summary>
    protected void ClearSelfCache()
    {
        isShowing = false;
        onHideCallback?.Invoke();
        UIManager.Instance.ClearPanelCache(this.GetType());
    }
}

// protected override void Init()
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
