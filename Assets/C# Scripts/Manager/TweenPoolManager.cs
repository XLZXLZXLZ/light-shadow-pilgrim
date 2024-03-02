using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 一个管控传入池子的Tween的Manager
/// 传入Tween当前Pool为空时，表示当前地图正在发生改变
/// 等于是将MapUpdate的控制逻辑移交到这里管控
/// 所有会改变场景的TweenPush到这个池子里面就彳亍
/// </summary>
public class TweenPoolManager : Singleton<TweenPoolManager>
{
    #region 原生部分

    private List<Tween> CurrentTweens { get; set; } = new();
    // public event Action CurrentFinishedCallback;
    public bool IsEmpty => CurrentTweens.Count == 0;

    public void PushTween(Tween tween)
    {
        if (tween == null || tween.IsComplete() || CurrentTweens.Contains(tween)) return;
        print(nameof(PushTween));
        
        if(IsEmpty)
            EventManager.Instance.MapUpdate.StartStageEvent();
        CurrentTweens.Add(tween);
        tween.onComplete += () => OnTweenCompleted(tween);
    }

    private void OnTweenCompleted(Tween tween)
    {
        
        CurrentTweens.Remove(tween);
        if (IsEmpty)
        {
            print("OnTweenCompleted");
            EventManager.Instance.MapUpdate.FinishStageEvent();
        }
    }

    #endregion
}

public static class TweenPoolExtension
{
    public static void PushToTweenPool(this Tween tween)
    {
        TweenPoolManager.Instance.PushTween(tween);
    }
}

