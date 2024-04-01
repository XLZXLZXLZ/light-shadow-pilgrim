#if ODIN_INSPECTOR

using System;
using System.Collections.Generic;
using UnityEngine;

public interface IArgListener
{
    public ArgListenerData data { get; set; }
    public void OnPublisherStateChanged(ArgPackage argPackage);
}

#endif




