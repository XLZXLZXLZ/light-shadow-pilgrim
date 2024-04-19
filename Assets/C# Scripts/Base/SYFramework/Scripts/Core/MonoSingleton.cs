using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;


public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected virtual bool IsDontDestroyOnLoad => true;
    
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null || instance.IsUnityNull())
                instance = FindObjectOfType<T>();
            if (instance == null || instance.IsUnityNull())
                instance = new GameObject(typeof(T).Name).AddComponent<T>();
            return instance;
        }
        private set => instance = value;
    }
    public virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        if(IsDontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
}

public class SerializedMonoSingleton<T> : SerializedMonoBehaviour where T : MonoBehaviour
{
    public static T Instance;
    protected virtual bool IsDontDestroyOnLoad => true;
    public virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<T>();
            if(IsDontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this) Destroy(gameObject);
    }
}