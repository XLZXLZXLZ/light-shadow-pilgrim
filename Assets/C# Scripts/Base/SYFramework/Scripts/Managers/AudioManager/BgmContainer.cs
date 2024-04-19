using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(menuName = "SYFramework/BgmContainer",fileName = "BgmContainer")]
public class BgmContainer : SerializedScriptableObject
{
    [OdinSerialize, NonSerialized] public List<AudioBlockGroup> bgmGroups = new();
    
    // public void AddBgm(AudioBlock audioBlock)
    // {
    //     foreach (var item in bgms)
    //     {
    //         if (item == null)
    //         {
    //             bgms.Remove(item);
    //             continue;
    //         }
    //         if (item.GetName() == audioBlock.GetName()) return;
    //     }
    //     bgms.Add(audioBlock);
    // }
}
