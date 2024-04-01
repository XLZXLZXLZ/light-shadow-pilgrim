using System;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : new()
{
    public static T Instance
    {
        get
        {
            instance ??= new T();
            return instance;
        }
    }
    private static T instance;
}

