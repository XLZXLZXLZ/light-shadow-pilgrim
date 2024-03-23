using System;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousSchedule
{
    public bool IsInvokeOnStart { get; private set; }
    public float Interval { get; private set; }
    public int Times { get; private set; }
    public Action ContinuousAction { get; private set; }

    public ContinuousSchedule(float interval, int times, Action invokeAction, bool isInvokeOnStart = false)
    {
        IsInvokeOnStart = isInvokeOnStart;
        Interval = interval;
        Times = times;
        ContinuousAction = invokeAction;
    }

    public void InvokeAction()
    {
        ContinuousAction?.Invoke();
    }
}

