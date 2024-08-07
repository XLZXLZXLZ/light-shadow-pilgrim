using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    /// <summary>
    /// 进入该状态时调用
    /// </summary>
    void Enter();
    
    /// <summary>
    /// 物理更新（FixedUpdate）
    /// </summary>
    void PhysicsUpdate();
    
    /// <summary>
    /// 逻辑更新（Update）
    /// </summary>
    void LogicUpdate();

    /// <summary>
    /// 离开该状态时调用
    /// </summary>
    void Exit();
}
