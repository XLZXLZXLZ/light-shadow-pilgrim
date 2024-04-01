using System;
using System.Collections.Generic;
using UnityEngine;

public enum StageStage
{
    Running,
    Success,
    Fail,
}

public abstract class StageBase
{
    public StageEvent stageEvent = new();
    private StageStage state = StageStage.Running;
    private bool isStart = false;

    public StageStage Update()
    {
        if (!isStart)
        {
            isStart = true;
            stageEvent.StartStageEvent();
            OnEnter();
        }
        state = OnUpdate();
        if (state is StageStage.Success or StageStage.Fail)
        {
            isStart = false;
            stageEvent.FinishStageEvent();
            OnExit();
        }

        return state;
    }

    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract StageStage OnUpdate();
}

public sealed class RootStageBase : StageBase
{
    public StageBase child;
    public bool IsFinished { get; private set; }
    
    public override void OnEnter()
    {
        
    }

    public override void OnExit()
    {
        
    }

    public override StageStage OnUpdate()
    {
        return child.OnUpdate();
    }
}

public abstract class DecoratorStageBase : StageBase
{
    public StageBase child;
}

public abstract class CompositeStageBase : StageBase
{
    public List<StageBase> children = new();
}

public abstract class ActionNodeBase : StageBase
{
    
}

