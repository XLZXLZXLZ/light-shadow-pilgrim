#if ODIN_INSPECTOR

using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct PLStateCase
{
    [field: OdinSerialize] public List<PALStateCaseItem> palCaseItems { get; private set; }
    [OdinSerialize] public UnityAction action;

    public bool IsMatchCase(Dictionary<IStatePublisher, int> stateDic)
    {
        if (palCaseItems.IsNullOrEmpty()) return false;
        foreach (var palCaseItem in palCaseItems)
        {
            if (!stateDic.ContainsKey(palCaseItem.statePublisher)) return false;
            if (stateDic[palCaseItem.statePublisher] != palCaseItem.State) return false;
        }
        
        return true;
    }
}

[Serializable]
public struct PALStateCaseItem
{
    [field: OdinSerialize] public IStatePublisher statePublisher { get; private set; }
    [field: OdinSerialize] public int State { get; private set; }
}

#endif
