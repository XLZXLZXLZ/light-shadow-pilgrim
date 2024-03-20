using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    //对象池根节点
    [SerializeField]
    private GameObject poolRootGameObject;

    /// <summary>
    /// GameObject池
    /// </summary>
    public Dictionary<string, GameObjectPool> gameObjectPool = new Dictionary<string, GameObjectPool>();

    /// <summary>
    /// Object池
    /// </summary>
    public Dictionary<string, ObjectPool> objectPool = new Dictionary<string, ObjectPool>();

    /// <summary>
    /// 初始化函数
    /// </summary>
    protected void Start()
    {
        if (poolRootGameObject == null)
        {
            poolRootGameObject = Instantiate(new GameObject(), null);
            poolRootGameObject.name = "PoolRoot";
        }
        DontDestroyOnLoad(poolRootGameObject);
    }
    
    

    /// <summary>
    /// 通过给定的预制体得到一个物体
    /// 1.如果对象池有，就拿一个出来
    /// 2.若果对象池没有，就Instantiate一个
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject GetGameObject(GameObject prefab, Transform parent = null)// where T : UnityEngine.Object
    {
        GameObject gameObject = null;

        if (CheckCache(prefab))
            gameObject = gameObjectPool[prefab.name].GetGameObject(parent);
        else
        {
            gameObject = Instantiate(prefab,parent);
            gameObject.name = prefab.name;
        }
        
        return gameObject;
    }

    /// <summary>
    /// 直接获取组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="prefab"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public T GetGameObject<T>(GameObject prefab, Transform parent = null) where T : UnityEngine.Object
    {
        T component = null;
        GameObject gameObject = GetGameObject(prefab,parent);
        if (gameObject != null)
            component = gameObject.GetComponent<T>();
        return component;
    }

    public T GetGameObject<T>(T prefab, Transform parent = null) where T : Component
    {
        GameObject gameObject = GetGameObject(prefab.gameObject,parent);
        return gameObject.GetComponent<T>();
    }
    
    // /// <summary>
    // /// 通过给定的路径拿一个物体
    // /// 1.如果对象池有，就拿一个出来
    // /// 2.若果对象池没有，就自动调用ResourceManager去加载一个，再生成
    // /// </summary>
    // /// <param name="prefabPath"></param>
    // /// <param name="parent"></param>
    // /// <returns></returns>
    // public GameObject GetGameObject(string prefabPath, Transform parent = null)
    // {
    //     string prefabName;
    //     int removeIndex = prefabPath.LastIndexOf("/");
    //     prefabName = prefabPath;
    //     if(removeIndex != -1)
    //     {
    //         prefabName = prefabPath.Remove(0, removeIndex + 1);
    //     }
    //
    //     GameObject gameObject = null;
    //     if (CheckCache(prefabName))
    //     {
    //         gameObject = gameObjectPool[prefabName].GetGameObject(parent);
    //         return gameObject;
    //     }
    //
    //     GameObject q = ResourceManager.Instance.LoadAsset<GameObject>(prefabPath);
    //     gameObject = Instantiate(q , parent);
    //     gameObject.name = q.name;
    //
    //     return gameObject;
    //
    // }
    
    /// <summary>
    /// 放入对象池
    /// </summary>
    /// <param name="prefab"></param>
    public void PushGameObject(GameObject prefab) 
    {
        if(gameObjectPool.ContainsKey(prefab.name))
        {
            gameObjectPool[prefab.name].PushGameObject(prefab);
        }
        else
        {
            gameObjectPool.Add(prefab.name, new GameObjectPool(prefab,poolRootGameObject));
        }
    }

    public T GetObject<T>() where T:class, new()
    {
        T forReturn=null;
        string fullName = typeof(T).FullName;
        if (objectPool.ContainsKey(fullName) && objectPool[fullName].objectQueue.Count > 0)
        {
            forReturn = (T)objectPool[fullName].GetObject();
        }
        else
        {
            forReturn = new T();
        }
        return forReturn;
    }

    public void PushObject(object obj)
    {
        string fullName = obj.GetType().FullName;
        if (objectPool.ContainsKey(fullName))
        {
            objectPool[fullName].PushObject(obj); 
        }
        else
        {
            objectPool.Add(fullName, new ObjectPool(obj));
        }
    }

    
    
    public bool CheckCache(string prefabName)
    {
        return gameObjectPool.ContainsKey(prefabName) && gameObjectPool[prefabName].gameObjectQueue.Count > 0;
    }
    public bool CheckCache(GameObject prefab)
    {
        return CheckCache(prefab.name);
    }
    
    public void ClearGameObjectPool()
    {
        gameObjectPool.Clear();
    }

    public void ClearObjectPool()
    {
        objectPool.Clear();
    }
}

