using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EffectInfo
{ 
    
}

[Serializable]
public class BackGroundInfo
{
    public float lightIntensity;
    public float darkIntensity;
    public Color lightColor;
    public Color darkColor;
    public Color lightAmbientColor;
    public Color darkAmbientColor;
}

public class EffectManager : MonoBehaviour
{
    [SerializeField]
    private BackGroundInfo backGroundInfo;

    [SerializeField]
    private GameObject clickEffect;
    private Color currentClickEffectColor = Color.white * 32;

    private Light gameLight;
    private float LightIntensity
    {
        get { return gameLight.intensity; }
        set { gameLight.intensity = value; }
    }

    private void Awake()
    {
        EventManager.Instance.OnPlayerLightStateChanged += LightStateChange;
        EventManager.Instance.OnClickNode += UseClickEffect;
        gameLight = GlobalLight.Instance.GetComponent<Light>();
    }

    private void LightStateChange(LightState state)
    {
        var color = state == LightState.Light ? backGroundInfo.lightColor : backGroundInfo.darkColor;
        var ambientColor = state == LightState.Light ? backGroundInfo.lightAmbientColor :backGroundInfo.darkAmbientColor;
        var lightIntensity = state == LightState.Light ? backGroundInfo.lightIntensity : backGroundInfo.darkIntensity;

        //修改雾颜色
        DOTween.To(
           () => RenderSettings.fogColor,
           x => RenderSettings.fogColor = x,
           color,
           0.3f
           ).PushToTweenPool(EventManager.Instance.MapUpdate);

        //修改环境色
        DOTween.To(
           () => RenderSettings.ambientSkyColor,
           x => RenderSettings.ambientSkyColor = x,
           ambientColor,
           0.3f
           ).PushToTweenPool(EventManager.Instance.MapUpdate);

        //修改背景色
        DOTween.To(
           () => Camera.main.backgroundColor,
           x => Camera.main.backgroundColor = x,
           color,
           0.3f
           ).PushToTweenPool(EventManager.Instance.MapUpdate);

        //修改光强度
        DOTween.To(
           () => LightIntensity,
           x => LightIntensity = x,
           lightIntensity,
           0.3f
           ).PushToTweenPool(EventManager.Instance.MapUpdate);

        var player = Player.Instance;
        var renderer = player.GetComponent<Renderer>();
        var particle = player.GetComponentInChildren<ParticleSystem>().GetComponent<Renderer>();
        

        if (state == LightState.Light)
        {
            renderer.material.DOColor(Color.white * 32, "_EmissionColor", 0.3f).SetEase(Ease.InQuart);
            particle.material.DOColor(Color.white * 32, "_EmissionColor", 0.3f).SetEase(Ease.InQuart);
            currentClickEffectColor = Color.white * 32;
        }
        else
        {
            renderer.material.DOColor(Color.black, "_EmissionColor", 0.3f).SetEase(Ease.OutQuart);
            particle.material.DOColor(Color.black, "_EmissionColor", 0.3f).SetEase(Ease.OutQuart);
            currentClickEffectColor = Color.black;
        }

    }

    private void UseClickEffect(PathNode node)
    {
        var go = Instantiate(clickEffect, node.transform.position, clickEffect.transform.rotation);
        var click = go.GetComponentInChildren<ParticleSystem>().GetComponent<Renderer>();
        click.material.SetColor("_EmissionColor", currentClickEffectColor);
    }
}
