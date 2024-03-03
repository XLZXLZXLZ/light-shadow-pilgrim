using System;
using System.Collections.Generic;
using UnityEngine;

public class Consts
{
    public const float VolumeFadeDuration = 0.3f;
    public const float LightStateIntensity = 0f;
    public const float DarkStateIntensity = 0.4f;

    public const float PlayerScaleTransformDuration = 0.5f;

    public static Color PlayerLightStateColor = new();
    public static Color PlayerDarkStateColor = new();
    public static Color LightSceneColor = new Color(225f/225f,225f/225f,182f/225f,225f/225f);
    public static Color DarkSceneColor = new Color(225f/225f,225f/225f,182f/225f,225f/225f);
}

