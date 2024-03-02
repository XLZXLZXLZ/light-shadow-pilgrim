using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 受光照影响的开关
/// </summary>
[RequireComponent(typeof(LightExtension))]
public class LightSwitch : Switch
{
    [SerializeField]
    private LightState whichStateIsOn = LightState.Dark; //决定光照开关在亮时打开还是暗时打开
    private LightExtension lightComponent;
    [SerializeField]
    private Renderer tipBall;
    [SerializeField]
    private ParticleSystem particle;

    private Color baseColor;

    private void Awake() //根据给定设置附加开关委托
    {
        lightComponent = GetComponent<LightExtension>();
        lightComponent.OnDarken += whichStateIsOn == LightState.Dark ? SwitchOn : SwitchOff;
        lightComponent.OnLighted += whichStateIsOn == LightState.Dark ? SwitchOff : SwitchOn;
        tipBall.material = new Material(tipBall.material);
        baseColor = tipBall.material.color;
    }

    private void Start()
    {
        SwitchOff();
    }

    protected override void SwitchOn() //修改法球亮度及粒子效果
    {
        base.SwitchOn();
        tipBall.material.DOColor(baseColor * 8, "_EmissionColor", 1);
        particle.Play();
    }

    protected override void SwitchOff()
    {
        base.SwitchOff();
        tipBall.material.DOColor(baseColor, "_EmissionColor", 1);
        particle.Stop();
    }
}
