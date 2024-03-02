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

public class StageEvent<T>
{
    public event Action<T> OnEarlyStart;
    public event Action<T> OnStart;
    public event Action<T> OnLateStart;
    
    public event Action<T> OnEarlyFinished;
    public event Action<T> OnFinished;
    public event Action<T> OnLateFinished;

    public void StartStageEvent(T arg)
    {
        OnEarlyStart?.Invoke(arg);
        OnStart?.Invoke(arg);
        OnLateStart?.Invoke(arg);
    }

    public void FinishStageEvent(T arg)
    {
        OnEarlyFinished?.Invoke(arg);
        OnFinished?.Invoke(arg);
        OnLateFinished?.Invoke(arg);
    }
}

public class StageEvent<T1,T2>
{
    public event Action<T1,T2> OnEarlyStart;
    public event Action<T1,T2> OnStart;
    public event Action<T1,T2> OnLateStart;
    
    public event Action<T1,T2> OnEarlyFinished;
    public event Action<T1,T2> OnFinished;
    public event Action<T1,T2> OnLateFinished;

    public void StartStageEvent(T1 arg1,T2 arg2)
    {
        OnEarlyStart?.Invoke(arg1,arg2);
        OnStart?.Invoke(arg1,arg2);
        OnLateStart?.Invoke(arg1,arg2);
    }

    public void FinishStageEvent(T1 arg1,T2 arg2)
    {
        OnEarlyFinished?.Invoke(arg1,arg2);
        OnFinished?.Invoke(arg1,arg2);
        OnLateFinished?.Invoke(arg1,arg2);
    }
}

