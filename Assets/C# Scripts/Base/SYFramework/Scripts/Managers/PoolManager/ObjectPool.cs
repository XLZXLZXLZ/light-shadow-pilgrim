using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public Queue<object> objectQueue=new Queue<object>();
    
    public ObjectPool(object obj)
    {
        PushObject(obj);
    }

    public void PushObject(object obj)
    {
        objectQueue.Enqueue(obj);
    }

    public object GetObject()
    {
        return objectQueue.Dequeue();
    }
}