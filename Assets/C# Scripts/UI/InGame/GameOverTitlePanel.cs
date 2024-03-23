using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[PanelConfig(
    typeof(GameOverTitlePanel),
    nameof(GameOverTitlePanel),
    3,
    true)]
public class GameOverTitlePanel : PanelBase
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI exitTipText;
    private bool isHiding = false;
    // private string exitTip;

    protected override void OnInit()
    {
        
    }

    protected override void OnShow()
    {
        titleText.color = Consts.TransparentColor;
        exitTipText.color = Consts.TransparentColor;
        titleText.DOColor(Color.white, Consts.UITitleFadeInOutDuration)
            .OnComplete(() =>
            {
                if (isHiding) return;
                exitTipText.DOColor(Consts.ReturnTipColor, Consts.UITitleFadeInOutDuration);
            });
    }

    protected override void OnHide()
    {
        DOTween.Sequence()
            .Append(titleText.DOColor(Consts.TransparentColor, Consts.UITitleFadeInOutDuration))
            .Join(exitTipText.DOColor(Color.clear, Consts.UITitleFadeInOutDuration))
            .OnComplete(ClearSelfCache);

        Cover.Instance.ChangeScene("MainMenu", 2, 1);
    }

    protected override void ShowAnim()
    {
        
    }

    protected override void HideAnim()
    {
        
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            isHiding = true;
            HideSelf();
        }
    }

    public void SetTip(string title)
    {
        titleText.text = title;
    }
}

