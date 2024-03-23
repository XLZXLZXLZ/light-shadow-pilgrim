using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

public class PanelCacheInfo
{
    public PanelConfigInfo ConfigInfo { get; private set; }
    public PanelBase PanelCache { get; private set; }
    public bool IsCache => PanelCache != null;
    public bool IsCaching => !handle.IsDone;
    private AsyncOperationHandle<GameObject> handle;
    private bool isActiveTemp;  // 设置Panel的isActive属性缓冲值，防止同一帧下操作随机执行顺序带来的Bug
    
    public PanelCacheInfo(PanelConfigInfo configInfo)
    {
        this.ConfigInfo = configInfo;
    }

    /// <summary>
    /// 同步加载Panel的缓存，如果Panel在远端则可能会卡顿（不是很推荐同步加载，最好在Panel在本地时使用）
    /// </summary>
    public T LoadCache<T>(bool active, Transform parent) where T: PanelBase
    {
        return LoadCache(typeof(T), active, parent) as T;
    }

    /// <summary>
    /// 同步加载Panel的缓存，如果Panel在远端则可能会卡顿（不是很推荐同步加载，最好在Panel在本地时使用）
    /// </summary>
    public PanelBase LoadCache(Type type, bool active, Transform parent)
    {
        if (handle.IsValid())
        {
#if UNITY_EDITOR
            SYLog.LogError("PanelCacheInfo：尝试缓存已经缓存的Panel");
#endif
            return default;
        }

        handle = Addressables.LoadAssetAsync<GameObject>(ConfigInfo.assetPath);
        handle.WaitForCompletion();
        
        GameObject panelCacheGo = GameObject.Instantiate(handle.Result,parent);
        panelCacheGo.name = handle.Result.name;
        panelCacheGo.SetActive(active);
        PanelCache = panelCacheGo.GetComponent<PanelBase>();
#if UNITY_EDITOR
        if (PanelCache == null)
        {
            SYLog.LogError("PanelCacheInfo：BYD，怎么有人连脚本都不挂啊？");
        }
#endif
        return PanelCache;
    }

    /// <summary>
    /// 异步加载Panel的缓存
    /// </summary>
    public AsyncOperationHandle<GameObject> LoadCacheAsync(bool active, Transform parent)
    {
        if (IsCaching)
        {
            isActiveTemp = active;
            return handle;
        }
        
        if (handle.IsValid())
        {
#if UNITY_EDITOR
            SYLog.LogError("PanelCacheInfo：尝试缓存已经缓存的Panel");
#endif
            return default;
        }
        
        handle = Addressables.LoadAssetAsync<GameObject>(ConfigInfo.assetPath);
        isActiveTemp = active;

        handle.Completed += go =>
        {
            // GameObject panelCache = PoolManager.Instance.GetGameObject(go.Result,parent);
            GameObject panelCacheGo = GameObject.Instantiate(go.Result,parent);
            panelCacheGo.name = go.Result.name;
            panelCacheGo.SetActive(isActiveTemp);
            PanelCache = panelCacheGo.GetComponent<PanelBase>();
        };
        return handle;
    }

    /// <summary>
    /// 保存Panel的缓存
    /// </summary>
    public void SaveCache()
    {
        if (PanelCache == null || PanelCache.gameObject == null)
        {
#if UNITY_EDITOR
            SYLog.LogError("PanelCacheInfo：在不合适的时机尝试缓存Panel");
#endif
            return;
        }
        PanelCache.OnSaveCacheStarted();
        PanelCache.gameObject.SetActive(false);
        PanelCache.OnSaveCacheFinished();
    }
    
    /// <summary>
    /// 清理Panel的缓存
    /// </summary>
    public void ClearCache()
    {
        if (PanelCache == null)
        {
#if UNITY_EDITOR
            SYLog.LogError("PanelCacheInfo：尝试销毁没有缓存的Panel");
#endif
            return;
        }
        PanelCache.OnClearCacheStarted();
        GameObject.Destroy(PanelCache.gameObject);
        PanelCache.OnClearCacheFinished();
        
        if(handle.IsValid())
            Addressables.Release(handle);
    }
}

