using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 暗庭（给脚本取名字真麻烦）
/// </summary>
[RequireComponent(typeof(LightExtension))]
public class TransformSwitch : Switch, ITriggerable
{
    private Animator anim;
    private LightExtension lightExtension;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        lightExtension = GetComponent<LightExtension>();
    }
    
    public void OnTrigger()
    {
        SwitchOn();
        anim.Play("SwitchOn");

        LightState lightState = GameManager.Instance.CurrentPlayerState == LightState.Light
            ? LightState.Dark
            : LightState.Light;
        EventManager.Instance.OnPlayerLightStateChanged.Invoke(lightState);
        lightExtension.LightState = lightState;
    }

    public void OnTriggerOver()
    {
        SwitchOff();
        anim.Play("SwitchOff");
    }
}

