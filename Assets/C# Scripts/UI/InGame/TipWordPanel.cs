using DG.Tweening;
using MyExtensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[PanelConfig(
    typeof(TipWordPanel),
    nameof(TipWordPanel),
    1,
    true)]
public class TipWordPanel : PanelBase
{
    [SerializeField] private TextMeshProUGUI tmp;
    
    private Tween lastTween;
    
    protected override void OnInit()
    {

    }

    protected override void OnShow()
    {
        Anim();
    }

    protected override void OnHide()
    {
        
    }

    protected override void ShowAnim()
    {
        
    }
    
    protected override void HideAnim()
    {
        
    }

    public void ShowTipWord(string word)
    {
        if (PanelDisplayState != PanelDisplayState.Hide)
        {
            // 如果此时上面已经在显示了，则清理当前动画，先淡出再显示下一波动画
            lastTween.Kill();
            lastTween = DOTween.Sequence()
                .Append(tmp.DOColor(Color.white.GetTransparent(), Consts.TipWordFadeInoutDuration))
                .AppendCallback(() => tmp.text = word)
                .Append(Anim());
        }
        else
        {
            tmp.text = word;
            tmp.color = Color.white.GetTransparent();
            ShowSelf();
        }
    }

    /// <summary>
    /// 单次显示动画，如果期间没有其他动画插入则淡入淡出
    /// </summary>
    private Tween Anim()
    {
        lastTween = DOTween.Sequence()
            .Append(tmp.DOColor(Color.white, Consts.TipWordFadeInoutDuration))
            .AppendInterval(Consts.TipWordExitDuration)
            .Append(tmp.DOColor(Color.white.GetTransparent(), Consts.TipWordFadeInoutDuration));
        
        lastTween.onComplete += () =>
        {
            HideSelf();
            lastTween = null;
        };
        
        return lastTween;
    }

    
}
