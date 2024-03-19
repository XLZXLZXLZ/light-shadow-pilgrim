using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.Serialization;
using UnityEngine;

[Serializable]
public abstract class MainMenuStateBase : IState
{
    [Header("Base")]
    [OdinSerialize] protected LevelItemGroup levelItemGroup;        // 管理的按钮
    [OdinSerialize] protected Vector3 startCameraPos;               // 入场时相机位置
    [OdinSerialize] protected Vector3 startLightDirection;          // 入场时光照方向
    [OdinSerialize] protected float startGlobalLightIntensity;      // 入场时全局光照强度
    [OdinSerialize] protected Color32 startEnvironmentLightColor;   // 入场时环境光照强度
    
    // protected bool IsStartAnim { get; private set; }
    // protected bool IsMoveAnim { get; private set; }
    // protected bool IsCanAcceptInput => !(IsStartAnim || IsMoveAnim || IsSelectedItem);
    protected bool IsAnim { get; private set; }
    protected bool IsSelectedItem { get; private set; } = false;
    protected bool IsCanAcceptInput => !(IsAnim || IsSelectedItem);

    public virtual void Init()
    {
        levelItemGroup.Init();
        levelItemGroup.onMouseEnterLevelItem += OnMouseEnterLevelItem;
        levelItemGroup.onMouseExitLevelItem += OnMouseExitLevelItem;
        levelItemGroup.onSelectedLevelItem += OnSelectLevelItem;

        MainMenuManager.Instance.anim.OnStart += OnAnimStart;
        MainMenuManager.Instance.anim.OnFinished += OnAnimFinished;
        // // MainMenuManager.Instance.anim.OnStart += OnStartAnimStart;
        // MainMenuManager.Instance.anim.OnFinished += OnStartAnimFinished;
        // MainMenuManager.Instance.anim.OnStart += OnMoveAnimStart;
        // MainMenuManager.Instance.anim.OnFinished += OnMoveAnimFinished;
    }

    #region StateFunc

    public virtual void Enter()
    {
        // TweenPoolManager最好能够改成兼容StageEvent和Action的，但是我懒得改了QWQ
        new List<Tween>() {
            MainMenuManager.Instance.SetCameraPos(startCameraPos, Consts.MainMenuChapterDuration),
            MainMenuManager.Instance.SetLightDirection(startLightDirection, Consts.MainMenuChapterDuration),
            MainMenuManager.Instance.SetGlobalLightIntensity(startGlobalLightIntensity, Consts.MainMenuChapterDuration),
            MainMenuManager.Instance.SetEnvironmentLightColor(startEnvironmentLightColor, Consts.MainMenuChapterDuration) }
            .PushToTweenPool(MainMenuManager.Instance.anim);
    }
    
    public virtual void PhysicsUpdate(){}
    
    public virtual void LogicUpdate(){}
    
    public virtual void Exit(){}

    #endregion

    #region LevelItemEvents

    /// <summary>
    /// 当鼠标进入按钮
    /// </summary>
    /// <param name="levelItem"></param>
    protected virtual void OnMouseEnterLevelItem(LevelItem levelItem)
    {
        if (levelItem == null || !IsCanAcceptInput) return;
        MouseEnterLevelItem(levelItem);
    }

    protected abstract void MouseEnterLevelItem(LevelItem levelItem);

    /// <summary>
    /// 当鼠标离开按钮
    /// </summary>
    /// <param name="levelItem"></param>
    protected virtual void OnMouseExitLevelItem(LevelItem levelItem)
    {
        if (levelItem == null || !IsCanAcceptInput) return;
        MouseExitLevelItem(levelItem);
    }

    protected abstract void MouseExitLevelItem(LevelItem levelItem);

    /// <summary>
    /// 当鼠标点击按钮（OnMouseUp）
    /// </summary>
    /// <param name="levelItem"></param>
    protected virtual void OnSelectLevelItem(LevelItem levelItem)
    {
        if (levelItem == null) return;
        IsSelectedItem = true;
        MainMenuManager.Instance.ChooseLevelItem(levelItem);
    }

    #endregion
    
    #region Events
    private void OnAnimStart()
    {
        IsAnim = true;
    }
    
    private void OnAnimFinished()
    {
        IsAnim = false;
    }
    
    // private void OnStartAnimStart()
    // {
    //     IsStartAnim = true;
    // }
    //
    // private void OnStartAnimFinished()
    // {
    //     IsStartAnim = false;
    // }

    // private void OnMoveAnimStart()
    // {
    //     IsMoveAnim = true;
    // }
    //
    // private void OnMoveAnimFinished()
    // {
    //     IsMoveAnim = false;
    // }

    #endregion
    
}

