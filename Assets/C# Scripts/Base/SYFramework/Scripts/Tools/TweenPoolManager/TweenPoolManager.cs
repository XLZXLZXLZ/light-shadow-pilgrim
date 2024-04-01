using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.Utilities;
using UnityEngine;

public class TweenPoolManager : Singleton<TweenPoolManager>
{
    private Dictionary<StageEvent, List<Tween>> CurrentTweenDic { get; set; } = new();

    public Tween PushTween(Tween tween, StageEvent stageEvent)
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
        
        tween.onComplete += () => OnTweenCompleted(tween, stageEvent); 

        return tween;
    }

    public List<Tween> PushTween(List<Tween> tweens, StageEvent stageEvent)
    {
        if (tweens.IsNullOrEmpty() || stageEvent == null) return null;

        for (int i = 0; i < tweens.Count; i++)
        {
            if (tweens[i] == null)
            {
                tweens.RemoveAt(i);
                i--;
            }
        }
        
        if (CurrentTweenDic.TryGetValue(stageEvent, out List<Tween> tweenList))
        {
            tweenList.AddRange(tweens);
        }
        else
        {
            CurrentTweenDic.Add(stageEvent,tweens);
            stageEvent.StartStageEvent();
        }

        tweens.ForEach(tween =>  tween.onComplete += () => OnTweenCompleted(tween, stageEvent));
        
        return tweens;
    }

    private void OnTweenCompleted(Tween tween, StageEvent stageEvent)
    {
        CurrentTweenDic[stageEvent].Remove(tween);
        if (CurrentTweenDic[stageEvent].Count == 0)
        {
            CurrentTweenDic.Remove(stageEvent);
            stageEvent.FinishStageEvent();
        }
    }

}

public static class TweenPoolExtension
{
    public static Tween PushToTweenPool(this Tween tween, StageEvent stageEvent)
    {
        return TweenPoolManager.Instance.PushTween(tween, stageEvent);
    }

    public static List<Tween> PushToTweenPool(this List<Tween> tweens, StageEvent stageEvent)
    {
        return TweenPoolManager.Instance.PushTween(tweens, stageEvent);
    }
}

