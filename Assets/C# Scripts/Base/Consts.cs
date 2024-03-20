using System;
using System.Collections.Generic;
using UnityEngine;

public class Consts
{
    public const float MainMenuChapterDuration = 1.5f;
    public const float MainMenuTransformDuration = 0.3f;
    public const float MainMenuCommonLightIntensity = 2f;

    public const float UITitleFadeInOutDuration = 3f;
    public const float UITitleExistDuration = 3f;
    public const float tipWordTimer = 15f;
    
    public const float VolumeFadeDuration = 0.3f;
    public const float LightStateIntensity = 0f;
    public const float DarkStateIntensity = 0.4f;

    public const float PlayerScaleTransformDuration = 0.5f;
    public const float GameOverCamUpHeight = 35f;
    public const float GameOverCamUpDuration = 2f;

    public const float LightSceneIntensity = 3f;
    public const float DarkSceneIntensity = 1f;

    public static Color PlayerLightStateColor = new();
    public static Color PlayerDarkStateColor = new();
    public static Color LightSceneColor = new Color(225f/225f,225f/225f,182f/225f,225f/225f);
    public static Color DarkSceneColor = new Color(56f/225f,48f/225f,37f/225f,225f/225f);
    public static Color TransparentColor = new Color(1f,1f,1f,0);
}

