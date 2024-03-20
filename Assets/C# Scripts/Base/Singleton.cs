using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    protected virtual bool IsDontDestroyOnLoad => false;

    private static T instance;
    public static T Instance
    {
        get 
        { 
            if (instance == null || instance.IsUnityNull())
            {
                instance = FindObjectOfType<T>();
                if (instance == null || instance.IsUnityNull())
                {
                    instance = new GameObject(typeof(T).Name).AddComponent<T>();
                }
                if(instance.IsDontDestroyOnLoad)
                    DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if(instance != null && instance != this)
            Destroy(gameObject);

        if (IsDontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
}
