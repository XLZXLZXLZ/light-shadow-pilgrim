using System;
using System.Collections.Generic;
using UnityEngine;

public class StageEvent
{
    public event Action OnEarlyStart;
    public event Action OnStart;
    public event Action OnLateStart;
    
    public event Action OnEarlyFinished;
    public event Action OnFinished;
    public event Action OnLateFinished;

    public void StartStageEvent()
    {
        OnEarlyStart?.Invoke();
        OnStart?.Invoke();
        OnLateStart?.Invoke();
    }

    public void FinishStageEvent()
    {
        OnEarlyFinished?.Invoke();
        OnFinished?.Invoke();
        OnLateFinished?.Invoke();
    }
}

