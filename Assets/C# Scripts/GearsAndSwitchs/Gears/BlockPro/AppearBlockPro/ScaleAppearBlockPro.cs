using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleAppearBlockPro : AppearBlockPro
{
    private Vector3 startScale;
    
    private void Start()
    {
        startScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }
    
    public override void SwitchOn()
    {
        DOTween.Sequence()
            .Append(transform.DOScale(startScale, duration))
            .PushToTweenPool(EventManager.Instance.MapUpdate);
    }

    public override void SwitchOff()
    {
        DOTween.Sequence()
            .Append(transform.DOScale(Vector3.zero, duration))
            .PushToTweenPool(EventManager.Instance.MapUpdate);
    }
}

