using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MyExtensions;
using UnityEngine;
using UnityEngine.UI;

[PanelConfig(
    typeof(PausePanel),
    nameof(PausePanel),
    4,
    false,
    KeyCode.Escape,
    KeyCode.Escape)]
public class PausePanel : PanelBase
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button backToMainMenuButton;
    [SerializeField] private Button exitGameButton;
    [SerializeField] private Image background;
    [SerializeField] private Graphic[] graphics;
    private const float Interval = 0.05f;
    private Color backgroundColor = new Color32(36,33,30,161);
    
    protected override void OnInit()
    {
        continueButton.onClick.AddListener(OnClickContinueButton);
        backToMainMenuButton.onClick.AddListener(OnClickBackToMainMenuButton);
        exitGameButton.onClick.AddListener(OnClickExitGameButton);
    }

    protected override void OnShow()
    {
        Time.timeScale = 0f;
    }

    protected override void OnHide()
    {
        Time.timeScale = 1f;
    }

    protected override void ShowAnim()
    {
        Sequence sequence = DOTween.Sequence().SetUpdate(true);

        background.color = backgroundColor.GetTransparent();
        sequence
            .AppendCallback(() => background.DOColor(backgroundColor, Consts.ButtonFadeInOutDuration).SetUpdate(true))
            .AppendInterval(Interval);
        
        for (int i = 0; i < graphics.Length; i++)
        {
            Graphic graphic = graphics[i];
            graphic.color = Color.white.GetTransparent();
            sequence
                .AppendCallback(() => graphic.DOColor(Color.white, Consts.ButtonFadeInOutDuration).SetUpdate(true))
                .AppendInterval(Interval);
        }
        
        sequence.onComplete += ShowAnimFinished;
    }

    protected override void HideAnim()
    {
        Sequence sequence = DOTween.Sequence().SetUpdate(true);
        for (int i = graphics.Length - 1; i >= 0; i--)
        {
            Graphic graphic = graphics[i];
            Color previewColor = graphic.color;
            sequence
                .AppendCallback(() => graphic.DOColor(Color.white.GetTransparent(), Consts.ButtonFadeInOutDuration).SetUpdate(true))
                .AppendInterval(Interval);
        }
        
        sequence
            .AppendCallback(() => background.DOColor(backgroundColor.GetTransparent(), Consts.ButtonFadeInOutDuration).SetUpdate(true))
            .AppendInterval(Interval);
        
        sequence.onComplete += HideAnimFinished;
    }

    private void OnClickContinueButton()
    {
        AudioManager.Instance.PlaySe("PausePanelClick");
        HideSelf();
    }
    
    private void OnClickBackToMainMenuButton()
    {
        // AudioManager.Instance.PlaySe("PausePanelClick");
        Cover.Instance.ChangeScene("MainMenu",1f,2f);
        HideSelf();
        
        UIManager.Instance.SetPanelCanControlByKeyCode<PausePanel>(false);
    }
    
    private void OnClickExitGameButton()
    {
        // AudioManager.Instance.PlaySe("PausePanelClick");
        Application.Quit();
    }
}
