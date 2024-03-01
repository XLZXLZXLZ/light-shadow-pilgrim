using System;
using System.Collections.Generic;

public abstract class StageBase
{
    public abstract List<Type> NextStageTypes { get; protected set; }
    
    public InvokableAction onStageEnterStart = new();
    public InvokableAction onStateEnterFinished = new();
    public InvokableAction onStageExitStart = new();
    public InvokableAction onStageExitFinished = new();

    public void OnEnter()
    {
        onStageEnterStart.Invoke();
        EnterLogic();
        onStateEnterFinished.Invoke();
    }

    public void OnExit()
    {
        onStageExitStart.Invoke();
        ExitLogic();
        onStageExitFinished.Invoke();
    }

    public abstract void EnterLogic();

    public abstract void ExitLogic();

    public abstract void OnUpdate();

}