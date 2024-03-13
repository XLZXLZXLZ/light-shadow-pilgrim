using System;
using System.Collections.Generic;
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
    
    public virtual void Init()
    {
        levelItemGroup.Init();
        levelItemGroup.onMouseEnterLevelItem += OnMouseEnterLevelItem;
        levelItemGroup.onMouseExitLevelItem += OnMouseExitLevelItem;
        levelItemGroup.onSelectedLevelItem += OnSelectLevelItem;
    }

    public virtual void Enter()
    {
        MainMenuManager.Instance.SetCameraPos(startCameraPos, Consts.MainMenuChapterDuration);
        MainMenuManager.Instance.SetLightDirection(startLightDirection, Consts.MainMenuChapterDuration);
        MainMenuManager.Instance.SetGlobalLightIntensity(startGlobalLightIntensity, Consts.MainMenuChapterDuration);
        MainMenuManager.Instance.SetEnvironmentLightColor(startEnvironmentLightColor, Consts.MainMenuChapterDuration);
    }
    
    public virtual void PhysicsUpdate(){}
    
    public virtual void LogicUpdate(){}
    
    public virtual void Exit(){}

    /// <summary>
    /// 当鼠标进入按钮
    /// </summary>
    /// <param name="levelItem"></param>
    protected abstract void OnMouseEnterLevelItem(LevelItem levelItem);

    /// <summary>
    /// 当鼠标离开按钮
    /// </summary>
    /// <param name="levelItem"></param>
    protected abstract void OnMouseExitLevelItem(LevelItem levelItem);

    /// <summary>
    /// 当鼠标点击按钮（OnMouseUp）
    /// </summary>
    /// <param name="levelItem"></param>
    protected virtual void OnSelectLevelItem(LevelItem levelItem)
    {
        MainMenuManager.Instance.ChooseLevelItem(levelItem);
    }
}

