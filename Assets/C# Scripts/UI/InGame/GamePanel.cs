using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[PanelConfig(
    typeof(GamePanel),
    nameof(GamePanel),
    3,
    false,
    true)]
public class GamePanel : PanelBase
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Vector2 pauseButtonOffset;
    
    private Vector2 startPos;
    private Vector2 upCenterPos;
    private bool isInit;
    protected override void OnInit()
    {
        pauseButton.onClick.AddListener(OnClickPauseButton);
    }

    protected override void OnShow()
    {
        
    }

    protected override void OnHide()
    {
        
    }

    protected override void ShowAnim()
    {
        if (!isInit)
        {
            startPos = pauseButton.transform.position;
            upCenterPos = startPos + pauseButtonOffset;
            isInit = true;
        }
        
        pauseButton.transform.position = upCenterPos;
        pauseButton.transform.DOMove(startPos, Consts.UIGamePanelAppearDuration)
            .onComplete += ShowAnimFinished;
    }

    protected override void HideAnim()
    {
        pauseButton.transform.DOMove(upCenterPos, Consts.UIGamePanelAppearDuration)
            .onComplete += HideAnimFinished;
    }

    private void OnClickPauseButton()
    {
        UIManager.Instance.ShowPanel<PausePanel>();
    }
}
