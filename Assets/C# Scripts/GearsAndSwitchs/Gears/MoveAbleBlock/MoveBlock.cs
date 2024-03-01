using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : Gear
{
    [SerializeField]
    private Ease ease = Ease.OutQuad;
    [SerializeField]
    private float duration = 1;
    [SerializeField]
    private Vector3 target;
    [SerializeField]
    private float delay;
    [SerializeField]
    private float shakeLevel; //是否引起相机震动

    private Vector3 origin;

    protected override void Awake()
    {
        base.Awake();
        origin = transform.position;
        target = transform.position + target;
    }

    protected override void SwitchOn() //更新地图，冲断移动信号
    {
        base.SwitchOn();

        EventManager.Instance.OnMapUpdateStart.Invoke();
        
        DOTween.Sequence()
            .AppendInterval(delay)
            .Append(transform.DOMove(target, duration).SetEase(ease))
            .Join(Camera.main.DOShakePosition(duration,shakeLevel,100))
            .OnComplete(() => EventManager.Instance.OnMapUpdateFinished.Invoke());
    }

    protected override void SwitchOff()
    {
        base.SwitchOff();

        EventManager.Instance.OnMapUpdateStart.Invoke();
        DOTween.Sequence()
            .AppendInterval(delay)
            .Append(transform.DOMove(origin, duration).SetEase(ease))
            .Join(Camera.main.DOShakePosition(duration * 1.2f, shakeLevel, 100))
            .OnComplete(() => EventManager.Instance.OnMapUpdateFinished.Invoke());
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + target);
        Gizmos.DrawWireSphere(transform.position + target,0.3f);
    }
}
