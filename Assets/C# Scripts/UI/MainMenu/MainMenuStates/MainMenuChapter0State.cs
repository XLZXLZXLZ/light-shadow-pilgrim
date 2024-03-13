using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MainMenuChapter0State : MainMenuStateBase
{
    protected override void OnMouseEnterLevelItem(LevelItem levelItem)
    {
        MainMenuManager.Instance.RotateLight(levelItem.LevelIndex * levelItemGroup.RotateAngle);
    }

    protected override void OnMouseExitLevelItem(LevelItem levelItem)
    {
        
    }
}

