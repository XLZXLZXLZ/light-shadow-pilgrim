using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveAppearBlockPro : AppearBlockPro
{
    [SerializeField] private float rotateAngle;
    [SerializeField] private Ease ease; 
    [SerializeField] private float hideHeight = 40;
    
    private Vector3 appearPos; //出现时，应当处于哪个位置
    private Vector3 hidePos;  //消失时，应当处于哪个位置
    private Vector3 appearAngle; //出现时，最终应当旋转的角度
    private Vector3 hideAngle; //消失时，最终应当旋转的角度
    
    protected override void Awake()
    {
        base.Awake();
        appearPos = transform.position;
        appearAngle = transform.eulerAngles + Vector3.up * rotateAngle;
        hidePos = transform.position + Vector3.down * hideHeight;
        hideAngle = transform.eulerAngles;
        transform.position = hidePos;
    }
    
    public override void SwitchOn()
    {
        DOTween.Sequence()
            .Append(transform.DOMove(appearPos, duration))
            .Join(transform.DORotate(appearAngle, 2f))
            .PushToTweenPool(EventManager.Instance.MapUpdate);
    }

    public override void SwitchOff()
    {
        DOTween.Sequence()
            .Append(transform.DOMove(hidePos, duration))
            .Join(transform.DORotate(hideAngle * rotateAngle, 2f))
            .PushToTweenPool(EventManager.Instance.MapUpdate);
    }
}

