using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

/// <summary>
/// 暗庭（给脚本取名字真麻烦）
/// </summary>
[RequireComponent(typeof(LightExtension))]
public class TransformSwitch : Switch, ITriggerable , IRepeatTriggerable
{
    private Animator anim;
    private LightExtension lightExtension;

    [SerializeField]
    private Renderer ColorObj;

    [SerializeField]
    private Renderer OutLineObj;

    [SerializeField]
    private Color darkColor;

    [SerializeField]
    private Color originColor;

    [SerializeField]
    private Color lightColor;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        lightExtension = GetComponent<LightExtension>();

        EventManager.Instance.OnPlayerLightStateChanged += OnLightStateChange;
    }

    private void OnLightStateChange(LightState state)
    {
        if (state == LightState.Light)
        {
            ColorObj.material.DOColor(Color.black, "_EmissionColor", 0.3f).SetEase(Ease.OutQuart);
            ColorObj.material.DOColor(Color.black, 0.3f).SetEase(Ease.OutQuart);
            OutLineObj.material.DOColor(originColor, 0.3f).SetEase(Ease.OutQuart);
        }
        else
        {
            ColorObj.material.DOColor(Color.white * 32, "_EmissionColor", 0.3f).SetEase(Ease.OutQuart);
            ColorObj.material.DOColor(Color.white, 0.3f).SetEase(Ease.OutQuart);
            OutLineObj.material.DOColor(darkColor, 0.3f).SetEase(Ease.OutQuart);
        }
    }
    
    public void OnTrigger()
    {
        SwitchOn();
        //anim.Play("SwitchOn");

        LightState lightState = GameManager.Instance.CurrentPlayerState == LightState.Light
            ? LightState.Dark
            : LightState.Light;
        EventManager.Instance.OnPlayerLightStateChanged.Invoke(lightState);
        lightExtension.LightState = lightState;


    }

    public void OnTriggerOver()
    {
        SwitchOff();
        //anim.Play("SwitchOff");
    }

    public void RepeatTrigger()
    {
        OnTrigger();
    }
}

