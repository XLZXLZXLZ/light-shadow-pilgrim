using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 所有状态机的基类
/// 请勿在子类中使用 Update() 和 FixedUpdate()
/// </summary>
public class StateMachine: SerializedMonoBehaviour
{
    //状态字典，获取状态用，它将由子类的数组赋值
    protected Dictionary<System.Type, IState> stateDic;
    //当前状态
    private IState currentState;
    private IState lastState;

    protected virtual void Update()
    {
        currentState?.LogicUpdate();
    }

    protected virtual void FixedUpdate()
    {
        currentState?.PhysicsUpdate();
    }
    
    public virtual void Begin(IState state)
    {
        currentState = state;
        lastState = null;
        currentState.Enter();
    }

    public virtual void Begin(Type stateType)
    {
        Begin(stateDic[stateType]);
    }

    public virtual void Begin<T>() where T : IState
    {
        Begin(typeof(T));
    }
    
    public virtual void SwitchState(IState state)
    {
        currentState.Exit();
        lastState = currentState;
        currentState = state;
        currentState.Enter();
    }

    public virtual void SwitchState(System.Type stateType)
    {
        SwitchState(stateDic[stateType]);
    }
    
    public virtual void SwitchState<T>() where T : IState
    {
        SwitchState(typeof(T));
    }
}
