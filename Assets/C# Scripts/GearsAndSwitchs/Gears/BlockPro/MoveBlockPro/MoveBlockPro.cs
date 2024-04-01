using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveBlockPro : BlockProBase
{
    [SerializeField] private Ease ease = Ease.OutQuad;
    [SerializeField] private float duration = 1;
    [SerializeField] private Vector3 target;
    [SerializeField] private float shakeLevel; //是否引起相机震动

    private Vector3 origin;
    
    protected override void Awake()
    {
        base.Awake();
        origin = transform.position;
        target = transform.position + target;
    }
    
    public override void SwitchOn() //更新地图，冲断移动信号
    {
        DOTween.Sequence()
            .Append(transform.DOMove(target, duration).SetEase(ease))
            .Join(Camera.main.DOShakePosition(duration,shakeLevel,100))
            .PushToTweenPool(EventManager.Instance.MapUpdate);
    }

    public override void SwitchOff()
    {
        DOTween.Sequence()
            .Append(transform.DOMove(origin, duration).SetEase(ease))
            .Join(Camera.main.DOShakePosition(duration * 1.2f, shakeLevel, 100))
            .PushToTweenPool(EventManager.Instance.MapUpdate);
    }
}

