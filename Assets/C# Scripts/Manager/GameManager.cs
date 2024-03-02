using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int currentLevel;
    public LightState CurrentPlayerState { get; private set; } = LightState.Light;

    protected override void Awake()
    {
        base.Awake();
        EventManager.Instance.OnPlayerLightStateChanged += OnPlayerLightStateChanged;
    }

    private void Start()
    {
        EventManager.Instance.OnGameStart.Invoke();
    }

    private void OnPlayerLightStateChanged(LightState lightState)
    {
        CurrentPlayerState = lightState;
        
    }
}
