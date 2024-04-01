using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : ManagerBase<PoolManager>
{
    //对象池根节点
    [SerializeField]
    private GameObject poolRootGameObject;

    /// <summary>
    /// GameObject池
    /// </summary>
    public Dictionary<string, GameObjectPool> gameObjectPool = new();

    /// <summary>
    /// Object池
    /// </summary>
    public Dictionary<string, ObjectPool> objectPool = new();

    /// <summary>
    /// 继承了IPoolable的池子
    /// </summary>
    public Dictionary<string, PoolablePool> poolablePool = new();

    /// <summary>
    /// 初始化函数
    /// </summary>
    protected void Start()
    {
        if(poolRootGameObject == null)
            poolRootGameObject = Instantiate(new GameObject(), null);
        
        poolRootGameObject.name = "PoolRoot";
        DontDestroyOnLoad(poolRootGameObject);
    }

    #region GameObjectPool

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
        if (prefab == null)
        {
#if UNITY_EDITOR
            SYLog.LogError($"PoolManager：传入的GameObject为空！");
#endif
            return null;
        }
        
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
        if (prefab == null)
        {
#if UNITY_EDITOR
            SYLog.LogError($"PoolManager：传入的GameObject为空！");
#endif
            return null;
        }
        
        T component = null;
        GameObject gameObject = GetGameObject(prefab,parent);
        if (gameObject != null)
            component = gameObject.GetComponent<T>();
        return component;
    }

    public T GetGameObject<T>(T prefab, Transform parent = null) where T : Component
    {
        if (prefab == null)
        {
#if UNITY_EDITOR
            SYLog.LogError($"PoolManager：传入的GameObject为空！");
#endif
            return null;
        }
        
        GameObject gameObject = GetGameObject(prefab.gameObject,parent);
        return gameObject.GetComponent<T>();
    }
    
    /// <summary>
    /// 放入对象池
    /// </summary>
    /// <param name="prefab"></param>
    public void PushGameObject(GameObject prefab) 
    {
        if (prefab == null)
        {
#if UNITY_EDITOR
            SYLog.LogError($"PoolManager：传入的GameObject为空！");
#endif
            return;
        }
        
        if(gameObjectPool.ContainsKey(prefab.name))
        {
            gameObjectPool[prefab.name].PushGameObject(prefab);
        }
        else
        {
            gameObjectPool.Add(prefab.name, new GameObjectPool(prefab,poolRootGameObject));
        }
    }
    
    private bool CheckCache(GameObject prefab)
    {
        return gameObjectPool.ContainsKey(prefab.name) && gameObjectPool[prefab.name].gameObjectQueue.Count > 0;
    }

    public void ClearTargetGameObjectPool(GameObject prefab)
    {
        if (prefab == null || !gameObjectPool.TryGetValue(prefab.name, out GameObjectPool pool)) return;
        foreach (GameObject go in pool.gameObjectQueue)
        {
            Destroy(go);
        }
        gameObjectPool.Remove(prefab.name);
    }

    public void ClearGameObjectPool()
    {
        gameObjectPool.Clear();
    }

    #endregion

    #region ObjectPool

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
    
    public void ClearObjectPool()
    {
        objectPool.Clear();
    }

    #endregion

    #region PoolablePool

    public IPoolable GetPoolable(IPoolable poolable)
    {
        if (poolable == null)
        {
#if UNITY_EDITOR
            SYLog.LogError($"PoolManager：传入的IPoolable类型为空！");
#endif
            return null;
        }

        IPoolable getPoolable = CheckCache(poolable) ? 
            poolablePool[poolable.Name].GetPoolable() : 
            poolable.Instantiate();
        getPoolable.OnGetInPool();
        
        return getPoolable;
    }
    

    public void PushPoolable(IPoolable poolable)
    {
        if (poolable == null)
        {
#if UNITY_EDITOR
            SYLog.LogError($"PoolManager：传入的IPoolable类型为空！");
#endif
            return;
        }
        
        poolable.OnPushToPool();
        string poolableName = poolable.Name;
        if (poolablePool.ContainsKey(poolableName))
        {
            poolablePool[poolableName].PushPoolable(poolable);
        }
        else
        {
            poolablePool.Add(poolable.Name, new PoolablePool(poolable));
        }
    }
    
    private bool CheckCache(IPoolable poolable)
    {
        return poolablePool.ContainsKey(poolable.Name) && poolablePool[poolable.Name].poolables.Count > 0;
    }
    
    public void ClearIPool()
    {
        poolablePool.Clear();
    }

    #endregion
    
}

