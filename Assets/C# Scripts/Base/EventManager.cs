using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
    // protected override bool IsDontDestroyOnLoad => true;

    public InvokableAction OnGameStart = new();

    public InvokableAction OnGameOver = new();

    public InvokableAction OnGenerateMapStart = new();

    public InvokableAction OnGenerateMapFinished = new();

    public InvokableAction OnPlayerMoveStart = new();

    public InvokableAction OnPlayerMoveFinished = new();

    public StageEvent MapUpdate = new();

    public InvokableAction<LightState> OnPlayerLightStateChangedStart = new();
    public InvokableAction<LightState> OnPlayerLightStateChangedFinished = new();
    
    
    public UnityAction<PathNode> OnClickNode; //选定某一结点时
    
    public UnityAction<PathNode> OnMoveToNewNode; //当玩家移动到一个新结点时
}
