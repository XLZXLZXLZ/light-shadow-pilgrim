#if ODIN_INSPECTOR

using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

[Serializable]
public class ArgListenerData
{
    [field: OdinSerialize] public List<IArgPublisher> publishers = new();
}

#endif



