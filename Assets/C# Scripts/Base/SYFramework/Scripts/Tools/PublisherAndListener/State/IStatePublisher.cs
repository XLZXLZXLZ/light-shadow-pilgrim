#if ODIN_INSPECTOR

using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

public interface IStatePublisher
{
    public int InitState { get; set; }
    public event Action<IStatePublisher,int> OnStateChanged;
}

#endif


