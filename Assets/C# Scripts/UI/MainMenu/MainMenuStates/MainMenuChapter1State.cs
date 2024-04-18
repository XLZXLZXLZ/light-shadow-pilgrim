using DG.Tweening;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuChapter1State : MainMenuStateBase
{
    [Header("Chapter1")]
    [OdinSerialize] private float startRotateAngleX;
    [OdinSerialize] private Transform arrow;


    protected override void MouseEnterLevelItem(LevelItem levelItem)
    {
        Vector3 endValue = new(startRotateAngleX, levelItem.LevelIndex * levelItemGroup.RotateAngle, 0);
        MainMenuManager.Instance.SetLightDirection(endValue, Consts.MainMenuTransformDuration);
        arrow.DORotate(Vector3.up * levelItem.LevelIndex * levelItemGroup.RotateAngle, Consts.MainMenuTransformDuration);
    }

    protected override void MouseExitLevelItem(LevelItem levelItem)
    {
        
    }
}

