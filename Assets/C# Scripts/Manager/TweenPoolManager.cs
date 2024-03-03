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
    private Dictionary<StageEvent, List<Tween>> CurrentTweenDic { get; set; } = new();

    public Tween PushTween(StageEvent stageEvent, Tween tween)
    {
        if (tween == null ||
            tween.IsComplete() ||
            stageEvent == null) return tween;

        if (CurrentTweenDic.TryGetValue(stageEvent, out List<Tween> tweens))
        {
            tweens.Add(tween);
        }
        else
        {
            CurrentTweenDic.Add(stageEvent, new List<Tween>(){tween});
            stageEvent.StartStageEvent();
        }
        
        tween.onComplete += () => OnTweenCompleted(stageEvent, tween);

        return tween;
    }

    private void OnTweenCompleted(StageEvent stageEvent, Tween tween)
    {
        CurrentTweenDic[stageEvent].Remove(tween);
        if (CurrentTweenDic[stageEvent].Count == 0)
        {
            stageEvent.FinishStageEvent();
            CurrentTweenDic.Remove(stageEvent);
        }
    }
}

public static class TweenPoolExtension
{
    public static Tween PushToTweenPool(this Tween tween, StageEvent stageEvent)
    {
        return TweenPoolManager.Instance.PushTween(stageEvent,tween);
    }
}

