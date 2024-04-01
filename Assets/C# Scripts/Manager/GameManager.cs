using System;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoSingleton<GameManager>
{
    protected override bool IsDontDestroyOnLoad => false;
    
    public int currentLevel;
    public string gameStartTip;
    public string gameOverTip;
    public string CurrentLevelString => $"{(currentLevel - 1) / 8 + 1} - {currentLevel % 8}";
    public LightState CurrentPlayerState { get; private set; } = LightState.Light;

    public override void Awake()
    {
        base.Awake();
        EventManager.Instance.OnPlayerLightStateChanged += OnPlayerLightStateChanged;
        EventManager.Instance.OnGameStart += OnGameStart;
        EventManager.Instance.OnGameOver += OnGameOver;
        EventManager.Instance.OnGenerateMapFinished += OnMapGenerateFinished;
    }

    private void Start()
    {
        EventManager.Instance.OnGameStart.Invoke();
    }

    private void OnGameStart()
    {
        UIManager.Instance.GetPanelAsync<GameStartTitlePanel>(
            panel =>
            {
                panel.SetTitle(gameStartTip,CurrentLevelString);
                UIManager.Instance.ShowPanel<GameStartTitlePanel>();
            });
    }

    private void OnGameOver()
    {
        UIManager.Instance.SetPanelCanControlByKeyCode<PausePanel>(false);
        // UIManager.Instance.ShowPanel<GameOverTitlePanel>().SetTip(gameOverTip);
        UIManager.Instance.GetPanelAsync<GameOverTitlePanel>(
            panel =>
            {
                panel.SetTip(gameOverTip);
                UIManager.Instance.ShowPanel<GameOverTitlePanel>();
            });
    }
    
    private void OnPlayerLightStateChanged(LightState lightState)
    {
        CurrentPlayerState = lightState;
    }

    private void OnMapGenerateFinished()
    {
        UIManager.Instance.SetPanelCanControlByKeyCode<PausePanel>(true);
    }
}
