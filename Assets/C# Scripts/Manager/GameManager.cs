using System;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoSingleton<GameManager>
{
    protected override bool IsDontDestroyOnLoad => false;

    [Header("关卡记录")]
    public int currentLevel;

    [Header("关卡文字")]
    public string gameStartTip;
    public string gameOverTip;
    public string CurrentLevelString => $"{(currentLevel - 1) / 8 + 1} - {(currentLevel - 1) % 8 + 1}";
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
        UIManager.Instance.HidePanel<GamePanel>();
        // UIManager.Instance.ShowPanel<GameOverTitlePanel>().SetTip(gameOverTip);
        
        // 芝士原来按任意键返回菜单的逻辑
        // UIManager.Instance.GetPanelAsync<GameOverTitlePanel>(
        //     panel =>
        //     {
        //         panel.SetTip(gameOverTip);
        //         UIManager.Instance.ShowPanel<GameOverTitlePanel>();
        //     });
    }
    
    private void OnPlayerLightStateChanged(LightState lightState)
    {
        CurrentPlayerState = lightState;
    }

    private void OnMapGenerateFinished()
    {
        UIManager.Instance.SetPanelCanControlByKeyCode<PausePanel>(true);
        UIManager.Instance.ShowPanel<GamePanel>();
    }
}
