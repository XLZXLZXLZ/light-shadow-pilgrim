using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class MainMenuStateBase : IState
{
    [SerializeField] protected LevelItemGroup levelItemGroup;
    
    public virtual void Init()
    {
        levelItemGroup.Init();
        levelItemGroup.onMouseEnterLevelItem += OnMouseEnterLevelItem;
        levelItemGroup.onMouseExitLevelItem += OnMouseExitLevelItem;
        levelItemGroup.onSelectedLevelItem += OnSelectLevelItem;
    }
    
    public abstract void Enter();
    
    public abstract void PhysicsUpdate();
    
    public abstract void LogicUpdate();
    
    public abstract void Exit();

    protected abstract void OnMouseEnterLevelItem(LevelItem levelItem);

    protected abstract void OnMouseExitLevelItem(LevelItem levelItem);

    protected virtual void OnSelectLevelItem(LevelItem levelItem)
    {
        MainMenuManager.Instance.ChooseLevelItem(levelItem);
    }
}

