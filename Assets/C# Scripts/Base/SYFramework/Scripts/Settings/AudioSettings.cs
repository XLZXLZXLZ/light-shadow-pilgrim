using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AudioSettings
{
    public float globalVolume = 1f;
    public float bgmVolume = 1f;
    public float seVolume = 1f;
    public bool isBgmLoop = false;

    // 只能在Editor下设置
    public float fadeInOutDuration = 1f;
}

