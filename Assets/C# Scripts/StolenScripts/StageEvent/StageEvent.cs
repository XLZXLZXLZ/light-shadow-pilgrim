using System;
using System.Collections.Generic;
using UnityEngine;

public class StageEvent
{
    public event Action OnEarlyEnter;
    public event Action OnEnter;
    public event Action OnLateEnter;
    
    public event Action OnEarlyExit;
    public event Action OnExit;
    public event Action OnLateExit;

    public void EnterStageEvent()
    {
        OnEarlyEnter?.Invoke();
        OnEnter?.Invoke();
        OnLateEnter?.Invoke();
    }

    public void ExitStageEvent()
    {
        OnEarlyExit?.Invoke();
        OnExit?.Invoke();
        OnLateExit?.Invoke();
    }
}

