using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class VolumeManager : Singleton<VolumeManager>
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
    protected override void Awake()
    { 
        base.Awake();
        volume?.profile.TryGet(out vignette);
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

    public void UpdateVolumeState(bool isMatch)
    {
        if (volume == null)
            return;
        if (isMatch)
        {
            darkenTween?.Kill();
            SetVolumeIntensity(Consts.LightStateIntensity);
        }
        else
            SetVolumeIntensity(Consts.DarkStateIntensity)
                .onComplete += () =>
            {
                darkenTween?.Kill();
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
