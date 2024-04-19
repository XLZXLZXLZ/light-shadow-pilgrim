using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;

[Serializable]
public class AudioBlockGroup
{
    public string tag;
    [OdinSerialize] public List<AudioBlock> audios;
}

