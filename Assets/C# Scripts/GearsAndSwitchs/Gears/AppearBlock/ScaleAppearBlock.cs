using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAppearBlock : AppearBlock
{
    private Vector3 startScale;

    private void Start()
    {
        startScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    protected override void SwitchOn()
    {
        base.SwitchOn();
        
        EventManager.Instance.OnMapUpdateStart.Invoke();
        DOTween.Sequence()
            .AppendInterval(delay)
            .Append(transform.DOScale(startScale, duration))
            .onComplete += () => EventManager.Instance.OnMapUpdateFinished.Invoke();
    }

    protected override void SwitchOff()
    {
        base.SwitchOff();
        DOTween.Sequence()
            .AppendInterval(delay)
            .Append(transform.DOScale(Vector3.zero, duration))
            .onComplete += () => EventManager.Instance.OnMapUpdateFinished.Invoke();
    }
}
