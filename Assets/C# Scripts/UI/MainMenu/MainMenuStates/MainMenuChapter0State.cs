using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

[Serializable]
public class MainMenuChapter0State : MainMenuStateBase
{
    [Header("Chapter0")] 
    [OdinSerialize] private float startRotateAngleX;

    public override void Enter()
    {
        base.Enter();
        AudioManager.Instance.PlayBgm(AudioName.Chapter0BGM);
    }
    
    protected override void MouseEnterLevelItem(LevelItem levelItem)
    {
        Vector3 endValue = new(startRotateAngleX,levelItem.LevelIndex * levelItemGroup.RotateAngle,0);
        MainMenuManager.Instance.SetLightDirection(endValue, Consts.MainMenuTransformDuration);
    }

    protected override void MouseExitLevelItem(LevelItem levelItem)
    {
        
    }
}

