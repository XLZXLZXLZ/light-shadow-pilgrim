using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(menuName = "SYFramework/SYRuntimeSettings",fileName = "SYRuntimeSettings")]
public class SYRuntimeSettings : SerializedScriptableObject
{
    [OdinSerialize,NonSerialized] public AudioSettings audioSettings = new();
    
    [OdinSerialize,NonSerialized] public UISettings uiSettings = new();

    private void Reset()
    {
        audioSettings = new();
        uiSettings = new();
    }
}

