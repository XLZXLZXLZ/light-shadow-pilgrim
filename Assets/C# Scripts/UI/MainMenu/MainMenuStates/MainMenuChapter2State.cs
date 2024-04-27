using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;
using DG.Tweening;

public class MainMenuChapter2State : MainMenuStateBase
{
    [Header("Chapter2")]
    [OdinSerialize] private Light areaLight;
    [OdinSerialize] private float areaLightIntensity;
    [OdinSerialize] private Transform clockRoot;
    public override void Enter()
    {
        base.Enter();
        Vector3 endValue = new Vector3(clockRoot.transform.position.x, areaLight.transform.position.y,clockRoot.transform.position.z);
        areaLight.transform.position = endValue;
        areaLight.intensity = 0;
        DOTween.To(
            () => areaLight.intensity,
            intensity => areaLight.intensity = intensity,
            areaLightIntensity,
            Consts.MainMenuChapterDuration);
    }

    public override void Exit()
    {
        base.Exit();
        DOTween.To(
            () => areaLight.intensity,
            intensity => areaLight.intensity = intensity,
            0,
            Consts.MainMenuChapterDuration);
    }

    protected override void MouseEnterLevelItem(LevelItem levelItem)
    {
        Vector3 endValue = new Vector3(levelItem.transform.position.x, areaLight.transform.position.y,levelItem.transform.position.z);
        areaLight.transform.DOLocalMove(endValue, Consts.MainMenuTransformDuration);
    }

    protected override void MouseExitLevelItem(LevelItem levelItem)
    {
        // Vector3 endValue = new Vector3(clockRoot.transform.position.x, areaLight.transform.position.y,clockRoot.transform.position.z);
        // areaLight.transform.DOLocalMove(endValue, Consts.MainMenuTransformDuration);
    }

    protected override void OnSelectLevelItem(LevelItem levelItem)
    {
        base.OnSelectLevelItem(levelItem);
        //MainMenuManager.Instance.SetGlobalLightIntensity(Consts.MainMenuCommonLightIntensity, Consts.MainMenuTransformDuration);
    }
}

