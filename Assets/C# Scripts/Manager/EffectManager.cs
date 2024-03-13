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
    public Color lightColor;
    public Color darkColor;
}

public class EffectManager : MonoBehaviour
{
    [SerializeField]
    private BackGroundInfo backGround;

    private Light gameLight;
    private float LightIntensity
    {
        get { return gameLight.intensity; }
        set { gameLight.intensity = value; }
    }

    private void Awake()
    {
        EventManager.Instance.OnPlayerLightStateChanged += BackGroundChange;
        gameLight = GlobalLight.Instance.GetComponent<Light>();
    }

    private void BackGroundChange(LightState state)
    {
        var color = state == LightState.Light ? Consts.LightSceneColor : Consts.DarkSceneColor;
        var lightIntensity = state == LightState.Light ? Consts.LightSceneIntensity : Consts.DarkSceneIntensity;

        DOTween.To(
           () => RenderSettings.fogColor,
           x => RenderSettings.fogColor = x,
           color,
           0.3f
           ).PushToTweenPool(EventManager.Instance.MapUpdate);

        DOTween.To(
           () => Camera.main.backgroundColor,
           x => Camera.main.backgroundColor = x,
           color,
           0.3f
           ).PushToTweenPool(EventManager.Instance.MapUpdate);

        DOTween.To(
           () => LightIntensity,
           x => LightIntensity = x,
           lightIntensity,
           0.3f
           ).PushToTweenPool(EventManager.Instance.MapUpdate);
    }
}
