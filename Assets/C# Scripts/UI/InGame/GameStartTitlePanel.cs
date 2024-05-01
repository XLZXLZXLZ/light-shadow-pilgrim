using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

[PanelConfig(
    typeof(GameStartTitlePanel),
    nameof(GameStartTitlePanel),
    1,
    true,
    false)]
public class GameStartTitlePanel : PanelBase
{
    [SerializeField] private TextMeshProUGUI levelTitleText;
    [SerializeField] private TextMeshProUGUI chapterTitleText;

    protected override void OnInit()
    {
        
    }

    protected override void OnShow()
    {
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

    protected override void OnHide()
    {
        
    }

    protected override void ShowAnim()
    {
        
    }

    protected override void HideAnim()
    {
        
    }

    public void SetTitle(string title, string levelIndex)
    {
        levelTitleText.text = title;
        chapterTitleText.text = "Chapter" + levelIndex;
    }

}
