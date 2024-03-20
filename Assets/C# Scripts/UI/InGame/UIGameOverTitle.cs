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

    public override void OnShow()
    {
        base.OnShow();
        
        // titleText.color = Consts.TransparentColor;
        // exitTipText.color = Consts.TransparentColor;
        // titleText.DOColor(Color.white, Consts.UITitleFadeInOutDuration)
        //     .OnComplete(() =>
        //     {
        //         if (isHiding) return;
        //         exitTipText.DOColor(Color.white, Consts.UITitleFadeInOutDuration);
        //     });
        
        // titleText.color = Consts.TransparentColor;
        // exitTipText.color = Consts.TransparentColor;
        // titleText.(Color.white, Consts.UITitleFadeInOutDuration)
        //     .OnComplete(() =>
        //     {
        //         if (isHiding) return;
        //         exitTipText.DOColor(Color.white, Consts.UITitleFadeInOutDuration);
        //     });
    }

    public override void OnHide()
    {
        titleText.DOColor(Color.white, Consts.UITitleFadeInOutDuration);
        exitTipText.DOColor(Color.white, Consts.UITitleFadeInOutDuration);

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
        titleText.text = title;
    }
}

