#if ODIN_INSPECTOR

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IArgPublisher
{
    public event UnityAction<ArgPackage> OnStateChanged;
}


#endif


