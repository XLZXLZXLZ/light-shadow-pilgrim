using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private Volume volume;
    private Vignette vignette;
    private bool isPlayerInDarkness = false;
    private Tween darkenTween;
    private float Intensity
    {
        get => vignette.intensity.value;
        set => vignette.intensity.value = value;
    }
    private void Awake()
    { 
        volume.profile.TryGet(out vignette);
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.L))
    //     {
    //         UpdateVolumeState(true);
    //     }
    //     if (Input.GetKeyDown(KeyCode.D))
    //     {
    //         UpdateVolumeState(false);
    //     }
    // }

    public void UpdateVolumeState(LightState lightState)
    {
        if (lightState == LightState.Light)
        {
            darkenTween?.Kill();
            SetVolumeIntensity(Consts.LightStateIntensity);
        }
        else
            SetVolumeIntensity(Consts.DarkStateIntensity)
                .onComplete += () =>
            {
                darkenTween = SetVolumeIntensity(Consts.DarkStateIntensity + 0.01f)
                    .SetEase(Ease.Flash, 2f)
                    .SetLoops(-1);
            };
    }

    private Tween SetVolumeIntensity(float endValue)
    {
        return DOTween.To(
            () => Intensity,
            i => Intensity = i,
            endValue,
            Consts.VolumeFadeDuration);
    }
}
