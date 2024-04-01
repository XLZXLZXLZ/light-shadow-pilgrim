using System;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    abstract string Name { get; set;}
    void OnPushToPool();
    void OnGetInPool();
    IPoolable Instantiate();
}

