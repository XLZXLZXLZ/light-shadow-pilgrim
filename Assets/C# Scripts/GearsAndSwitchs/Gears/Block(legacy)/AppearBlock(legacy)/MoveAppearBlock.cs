using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAppearBlock : AppearBlock
{
    [SerializeField]
    [Tooltip("上升时配套的旋转，为0则不旋转")]
    private float rotateAngle;
    [SerializeField]
    [Tooltip("运行缓冲模式")]
    private Ease ease; 
    [SerializeField]
    [Tooltip("至少下降多少能到雾里")]
    private float hideHeight = 40;

    #region 自动计算DOTWEEN所需数值，是托屎山
    private Vector3 appearPos; //出现时，应当处于哪个位置
    private Vector3 hidePos;  //消失时，应当处于哪个位置
    private Vector3 appearAngle; //出现时，最终应当旋转的角度
    private Vector3 hideAngle; //消失时，最终应当旋转的角度
    #endregion

    #region 初始化数据
    private void Start()
    {
        appearPos = transform.position;
        appearAngle = transform.eulerAngles + Vector3.up * rotateAngle;
        hidePos = transform.position + Vector3.down * hideHeight;
        hideAngle = transform.eulerAngles;
        transform.position = hidePos;
    }
    #endregion

    protected override void SwitchOn()
    {
        base.SwitchOn();

        
        DOTween.Sequence()
            .AppendInterval(delay)
            .Append(transform.DOMove(appearPos, duration))
            .Join(transform.DORotate(appearAngle, 2f))
            .PushToTweenPool(EventManager.Instance.MapUpdate);

    }

    protected override void SwitchOff()
    {
        base.SwitchOff();
        
        DOTween.Sequence()
            .AppendInterval(delay)
            .Append(transform.DOMove(hidePos, duration))
            .Join(transform.DORotate(hideAngle * rotateAngle, 2f))
            .PushToTweenPool(EventManager.Instance.MapUpdate);
    }
}
