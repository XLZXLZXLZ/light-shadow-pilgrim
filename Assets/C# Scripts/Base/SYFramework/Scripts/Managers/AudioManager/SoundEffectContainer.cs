using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(menuName = "SYFramework/SoundEffectContainer",fileName = "SoundEffectContainer")]
public class SoundEffectContainer : SerializedScriptableObject
{
    [OdinSerialize,NonSerialized] public List<AudioBlockGroup> soundEffectGroups = new();

    // public AudioBlockSimple Simple;
    // public void AddSoundEffect(AudioBlock audioBlock)
    // {
    //     foreach (var item in soundEffects)
    //     {
    //         if (item == null)
    //         {
    //             soundEffects.Remove(item);
    //             continue;
    //         }
    //         if (item.GetName() == audioBlock.GetName()) return;
    //
    //     }
    //     soundEffects.Add(audioBlock);
    // }
}
