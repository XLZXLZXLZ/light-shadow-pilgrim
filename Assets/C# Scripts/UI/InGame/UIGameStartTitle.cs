using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIGameStartTitle : PanelBase
{
    [SerializeField] private TextMeshProUGUI levelTitleText;
    [SerializeField] private TextMeshProUGUI chapterTitleText;
    public override int panelSortingLayer => 1;
    public override bool isHideDirectly => true;

    public override void OnShow()
    {
        base.OnShow();
        
        levelTitleText.color = Consts.TransparentColor;
        chapterTitleText.color = Consts.TransparentColor;
        DOTween.Sequence()
            .Append(levelTitleText.DOColor(Color.white, Consts.UITitleFadeInOutDuration))
            .Join(chapterTitleText.DOColor(Color.white, Consts.UITitleFadeInOutDuration))
            .AppendInterval(Consts.UITitleExistDuration)
            .Append(levelTitleText.DOColor(Consts.TransparentColor, Consts.UITitleFadeInOutDuration))
            .Join(chapterTitleText.DOColor(Consts.TransparentColor, Consts.UITitleFadeInOutDuration))
            .onComplete += HideSelf;
    }

    // public override void OnHide()
    // {
    //     base.OnHide();
    // }

    public void SetTitle(string title, string levelIndex)
    {
        levelTitleText.text = title;
        chapterTitleText.text = "Chapter" + levelIndex;
    }

}
