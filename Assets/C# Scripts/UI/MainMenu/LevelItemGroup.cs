using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelItemGroup
{
    [field: SerializeField] public List<LevelItem> LevelItems { get; private set; } = new();
    [SerializeField] private Material highLightMat;
    [SerializeField] private int chapter = 0;
    public LevelItem CurrentSelectedItem { get; private set; }
    public float RotateAngle => 360f / LevelItems.Count;
    
    public void Init()
    {
        CurrentSelectedItem = null;
        
        LevelItems.ForEach(levelItem =>
        {
            if (StaticData.IsLevelCompleted(chapter * Consts.LevelCountEachChapter + levelItem.LevelIndex))
                levelItem.SetMat(highLightMat);

            levelItem.onMouseEnter += OnMouseEnterLevelItem;
            levelItem.onMouseExit += OnMouseExitLevelItem;
            levelItem.onMouseUp += OnMouseUpLevelItem;
        });
    }

    #region Events
    
    public event Action<LevelItem> onMouseEnterLevelItem;
    public event Action<LevelItem> onMouseExitLevelItem;
    public event Action<LevelItem> onSelectedLevelItem;

    private void OnMouseEnterLevelItem(LevelItem levelItem)
    {
        if(CurrentSelectedItem != null)
            CurrentSelectedItem.MoveDown();
        CurrentSelectedItem = levelItem;
        CurrentSelectedItem.MoveUp();
        
        onMouseEnterLevelItem?.Invoke(levelItem);
    }

    private void OnMouseExitLevelItem(LevelItem levelItem)
    {
        if(CurrentSelectedItem != null)
            CurrentSelectedItem.MoveDown();
        CurrentSelectedItem = null;
        
        onMouseExitLevelItem?.Invoke(levelItem);
    }
    
    private void OnMouseUpLevelItem(LevelItem levelItem)
    {
        onSelectedLevelItem?.Invoke(levelItem);
    }

    #endregion
    
    // /// <summary>
    // /// 为通关部分设置发光材质
    // /// </summary>
    // private void SetCompleteMat()
    // {
    //     levelItems.ForEach(item =>
    //     {
    //         if (StaticData.IsLevelCompleted(item.LevelIndex))
    //         {
    //             item.SetMat(highLightMat);
    //         }
    //     });
    // } 
}

