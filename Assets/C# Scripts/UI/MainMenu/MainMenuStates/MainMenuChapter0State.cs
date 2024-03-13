using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MainMenuChapter0State : MainMenuStateBase
{
    public override void Enter()
    {
        
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void LogicUpdate()
    {
        
    }

    public override void Exit()
    {
        
    }

    protected override void OnMouseEnterLevelItem(LevelItem levelItem)
    {
        MainMenuManager.Instance.RotateLight(levelItem.LevelIndex * levelItemGroup.RotateAngle);
    }

    protected override void OnMouseExitLevelItem(LevelItem levelItem)
    {
        
    }
}

