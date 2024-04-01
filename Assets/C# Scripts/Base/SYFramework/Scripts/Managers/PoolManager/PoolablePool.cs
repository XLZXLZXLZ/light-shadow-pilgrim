using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolablePool
{
    public Queue<IPoolable> poolables = new();

    public PoolablePool(IPoolable poolable)
    {
        PushPoolable(poolable);
    }

    public void PushPoolable(IPoolable poolable)
    {
        poolables.Enqueue(poolable);
    }

    public IPoolable GetPoolable()
    {
        return poolables.Dequeue();
    }
}

