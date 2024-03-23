using System;
using UnityEngine;

public class Schedule
{
    public float duration { get; private set; }
    public float startTime { get; private set; }
    public float endTime { get; private set; }
    
    private event Action onStartCallback;
    private event Action onEndCallback;
    
    public Schedule(float duration)
    {
        this.duration = duration;
        startTime = Time.realtimeSinceStartup;
        endTime = Time.realtimeSinceStartup + duration;
    }
    public Schedule(float duration,Action onEndCallback)
    {
        this.duration = duration;
        startTime = Time.realtimeSinceStartup;
        endTime = Time.realtimeSinceStartup + duration;
        
        this.onStartCallback = null;
        this.onEndCallback = onEndCallback;
    }
    public Schedule(float duration,Action onStartCallback,Action onEndCallback)
    {
        this.duration = duration;
        startTime = Time.realtimeSinceStartup;
        endTime = Time.realtimeSinceStartup + duration;
        
        this.onStartCallback = onStartCallback;
        this.onEndCallback = onEndCallback;
    }

    public void InvokeStartCallback()
    {
        onStartCallback?.Invoke();
    }

    public void InvokeEndCallback()
    {
        onEndCallback?.Invoke();
    }
    
}