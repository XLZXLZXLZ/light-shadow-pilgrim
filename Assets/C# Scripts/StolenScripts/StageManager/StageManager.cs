using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    private Dictionary<Type, StageBase> stageDic = new();
    private Queue<StageSwitchRequest> stageSwitchRequests = new();
    public StageBase CurrentStage { get; private set; }

    private void Update()
    {
        CurrentStage?.OnUpdate();
        
        if(stageSwitchRequests.Peek() != null)
            SwitchStage(stageSwitchRequests.Dequeue());
        // Debug.Log(CurrentStage);
    }

    public void PushSwitchRequest(Type stageType)
    {
        if (stageDic.TryGetValue(stageType, out StageBase stage))
            stageSwitchRequests.Enqueue(new StageSwitchRequest(stage));
        else 
            Debug.LogWarning($"没有找到类型为的{stageType}Stage");
    }

    private void StartStage(StageBase stage)
    {
        CurrentStage = stage;
        stage.OnEnter();
    }

    private void SwitchStage(StageSwitchRequest stageSwitchRequest)
    {
        SwitchStage(stageSwitchRequest.NextStage);
    }
    
    private void SwitchStage(StageBase stage)
    {
        CurrentStage?.OnExit();
        CurrentStage = stage;
        CurrentStage.OnEnter();
    }
    
    public StageBase GetCurrentStage()
    {
        return CurrentStage;
    }

    public T GetStage<T>() where T : StageBase
    {
        if (stageDic.TryGetValue(typeof(T), out StageBase stage))
            return (T)stage;
        else
            return default;
    }
    
}