using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : Singleton<GameManager>
{
    public int currentLevel;
    public string gameStartTip;
    public string gameOverTip;
    public string CurrentLevelString => $"{(currentLevel - 1) / 8 + 1} - {currentLevel % 8}";
    public LightState CurrentPlayerState { get; private set; } = LightState.Light;

    protected override void Awake()
    {
        base.Awake();
        EventManager.Instance.OnPlayerLightStateChanged += OnPlayerLightStateChanged;
        EventManager.Instance.OnGameStart += OnGameStart;
    }

    private void Start()
    {
        EventManager.Instance.OnGameStart.Invoke();
    }

    private void OnPlayerLightStateChanged(LightState lightState)
    {
        CurrentPlayerState = lightState;
    }

    private void OnGameStart()
    {
        UIManager.Instance.ShowPanel<UIGameStartTitle>()
            .SetTitle(gameStartTip,CurrentLevelString);

    }

    /// <summary>
    /// UI框架不完善，先这么写了
    /// 应该允许Panel有一个缓存来预设一些信息
    /// </summary>
    public void ShowGameOverTip()
    {
        UIManager.Instance.ShowPanel<UIGameOverTitle>()
            .SetTip(gameOverTip);
    }
}
