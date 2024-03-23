using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

public class PLManager : Singleton<PLManager>
{
    public void Init(IStateListener stateListener)
    {
        if (stateListener == null || stateListener.data == null) return;

        StateListenerData data = stateListener.data;
        data.currentStateDic = new();
        data.publishers.ForEach(publisher =>
        {
            publisher.OnStateChanged += data.OnPublisherStateChanged;
            data.currentStateDic.Add(publisher, publisher.InitState);
        });
    }

    public void Init(IArgListener argListener)
    {
        if (argListener == null || argListener.data == null) return;
        
        ArgListenerData data = argListener.data;
        argListener.data.publishers.ForEach(publisher =>
        {
            publisher.OnStateChanged += argListener.OnPublisherStateChanged;
        });

    }

    public void ClearCache(IStateListener stateListener)
    {
        stateListener.data.publishers.ForEach(
            publisher => publisher.OnStateChanged -= stateListener.data.OnPublisherStateChanged);
    }

    public void ClearCache(IArgListener argListener)
    {
        argListener.data.publishers.ForEach(
            publisher => publisher.OnStateChanged -= argListener.OnPublisherStateChanged);
    }
}

public static class PLExtension
{
    public static void Init(this IArgListener listener)
    {
        PLManager.Instance.Init(listener);
    }
    
    public static void Init(this IStateListener listener)
    {
        PLManager.Instance.Init(listener);
    }
}


