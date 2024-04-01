#if ODIN_INSPECTOR

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IStateListener
{
    public StateListenerData data { get; set; }
}

#endif





