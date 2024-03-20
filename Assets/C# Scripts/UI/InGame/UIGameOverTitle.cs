using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UIGameOverTitle : PanelBase
{
    public override int panelSortingLayer => 1;
    public override bool isHideDirectly => false;

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI exitTipText;
    private bool isHiding = false;
    private string exitTip;

    public override void OnShow()
    {
        base.OnShow();
        
        titleText.color = Consts.TransparentColor;
        exitTipText.color = Consts.TransparentColor;
        titleText.DOColor(Color.white, Consts.UITitleFadeInOutDuration)
            .OnComplete(() =>
            {
                if (isHiding) return;
                exitTipText.DOColor(Consts.ReturnTipColor, Consts.UITitleFadeInOutDuration);
            });
        
        // titleText.color = Consts.TransparentColor;
        // exitTipText.color = Consts.TransparentColor;
        // titleText.DOText(exitTip, Consts.UITitleFadeInOutDuration)
        //     .OnComplete(() =>
        //     {
        //         if (isHiding) return;
        //         exitTipText.DOColor(Color.white, Consts.UITitleFadeInOutDuration);
        //     });
    }

    public override void OnHide()
    {
        DOTween.Sequence()
            .Append(titleText.DOColor(Consts.TransparentColor, Consts.UITitleFadeInOutDuration))
            .Join(exitTipText.DOColor(Color.clear, Consts.UITitleFadeInOutDuration))
            .OnComplete(ClearSelfCache);

        Cover.Instance.ChangeScene("MainMenu", 2, 1);
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
        exitTip = title;
    }
}

