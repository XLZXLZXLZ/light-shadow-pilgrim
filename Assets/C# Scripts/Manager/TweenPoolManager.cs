using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 一个管控传入池子的Tween的Manager
/// 当所有的Tween全部执行完成，后会执行CurrentFinishedCallback
/// 这里为了适配当前项目将代码分成了两部分
/// </summary>
public class TweenPoolManager : Singleton<TweenPoolManager>
{
    #region 原生部分

    private List<Tween> CurrentTweens { get; set; } = new();
    public event Action CurrentFinishedCallback;
    public bool IsEmpty => CurrentTweens.Count == 0;

    public void PushTween(Tween tween)
    {
        if (tween == null || tween.IsComplete() || CurrentTweens.Contains(tween)) return;
        
        CurrentTweens.Add(tween);
        tween.onComplete += () => OnTweenCompleted(tween);
    }

    private void OnTweenCompleted(Tween tween)
    {
        CurrentTweens.Remove(tween);
        if(IsEmpty)
            CurrentFinishedCallback?.Invoke();
    }

    #endregion

    #region 适配项目部分

    // protected override void Awake()
    // {
    //     base.Awake();
    //     EventManager.Instance.OnMapUpdate.OnEarlyEnter += OnMapUpdateEarlyEnter;
    // }
    //
    // private void OnMapUpdateEarlyEnter()
    // {
    //     CurrentFinishedCallback = () =>
    //     {
    //         EventManager.Instance.OnMapUpdate.ExitStageEvent();
    //         CurrentFinishedCallback = null;
    //     };
    // }

    #endregion
    
}

public static class TweenPoolExtension
{
    public static void PushToTweenPool(this Tween tween)
    {
        TweenPoolManager.Instance.PushTween(tween);
    }
}

