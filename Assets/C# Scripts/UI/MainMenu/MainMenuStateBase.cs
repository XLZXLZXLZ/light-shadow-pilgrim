using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

[Serializable]
public abstract class MainMenuStateBase : IState
{
    [OdinSerialize] protected LevelItemGroup levelItemGroup;
    [OdinSerialize] protected Vector3 startCameraPos;
    [OdinSerialize] protected Vector3 startLightDirection;
    [OdinSerialize] protected float startLightIntensity;
    
    public virtual void Init()
    {
        levelItemGroup.Init();
        levelItemGroup.onMouseEnterLevelItem += OnMouseEnterLevelItem;
        levelItemGroup.onMouseExitLevelItem += OnMouseExitLevelItem;
        levelItemGroup.onSelectedLevelItem += OnSelectLevelItem;
    }

    public virtual void Enter()
    {
        MainMenuManager.Instance.SetCameraPos(startCameraPos);
        MainMenuManager.Instance.SetLightDirection(startLightDirection);
        MainMenuManager.Instance.SetLightIntensity(startLightIntensity);
    }
    
    public virtual void PhysicsUpdate(){}
    
    public virtual void LogicUpdate(){}
    
    public virtual void Exit(){}

    protected abstract void OnMouseEnterLevelItem(LevelItem levelItem);

    protected abstract void OnMouseExitLevelItem(LevelItem levelItem);

    protected virtual void OnSelectLevelItem(LevelItem levelItem)
    {
        MainMenuManager.Instance.ChooseLevelItem(levelItem);
    }
}

