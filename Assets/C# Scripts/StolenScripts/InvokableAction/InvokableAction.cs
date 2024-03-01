using System;
using System.Collections.Generic;
using UnityEngine;

public interface InvokableActionBase
{

}

public class InvokableAction : InvokableActionBase
{
    public event Action action;
    public void Invoke() => action?.Invoke();

    public static InvokableAction operator +(InvokableAction thisInvokableAction, Action action)
    {
        thisInvokableAction.action += action;
        return thisInvokableAction;
    }
    public static InvokableAction operator -(InvokableAction thisInvokableAction, Action action)
    {
        thisInvokableAction.action -= action;
        return thisInvokableAction;
    }

    public void Clear()
    {
        action = null;
    }
}

public class InvokableAction<T>: InvokableActionBase
{
    public event Action<T> action;
    public void Invoke(T arg) => action?.Invoke(arg);
    
    public static InvokableAction<T> operator +(InvokableAction<T> thisInvokableAction, Action<T> action)
    {
        thisInvokableAction.action += action;
        return thisInvokableAction;
    }
    public static InvokableAction<T> operator -(InvokableAction<T> thisInvokableAction, Action<T> action)
    {
        thisInvokableAction.action -= action;
        return thisInvokableAction;
    }
    
    public void Clear()
    {
        action = null;
    }
}

public class InvokableAction<T1, T2>: InvokableActionBase
{
    public event Action<T1,T2> action;
    public void Invoke(T1 arg1, T2 arg2) => action?.Invoke(arg1,arg2);
    
    public static InvokableAction<T1,T2> operator +(InvokableAction<T1,T2> thisInvokableAction, Action<T1,T2> action)
    {
        thisInvokableAction.action += action;
        return thisInvokableAction;
    }
    public static InvokableAction<T1,T2> operator -(InvokableAction<T1,T2> thisInvokableAction, Action<T1,T2> action)
    {
        thisInvokableAction.action -= action;
        return thisInvokableAction;
    }
    
    public void Clear()
    {
        action = null;
    }
}

public class InvokableAction<T1, T2, T3>: InvokableActionBase
{
    public event Action<T1,T2, T3> action;
    public void Invoke(T1 arg1, T2 arg2, T3 arg3) => action?.Invoke(arg1,arg2,arg3);
    
    public static InvokableAction<T1,T2, T3> operator +(InvokableAction<T1,T2, T3> thisInvokableAction, Action<T1,T2, T3> action)
    {
        thisInvokableAction.action += action;
        return thisInvokableAction;
    }
    public static InvokableAction<T1,T2, T3> operator -(InvokableAction<T1,T2, T3> thisInvokableAction, Action<T1,T2, T3> action)
    {
        thisInvokableAction.action -= action;
        return thisInvokableAction;
    }
    
    public void Clear()
    {
        action = null;
    }
}

