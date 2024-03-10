using DG.Tweening;
using MyExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 可操控的移动块，名字打错了，懒得改了
/// </summary>
public class ControllerableMoveBlock : Gear
{
    [SerializeField]
    private Vector3 moveAxis = Vector3.up;
    [SerializeField]
    private int top = 2, bottom = -2;
    [SerializeField]
    protected Renderer tipColor;
    [SerializeField]
    protected Color onColor, offColor;

    private int currentIndex = 0;

    #region 机关表现
    private void Start()
    {
        tipColor.material = new Material(tipColor.material); //创建临时材质，避免直接替换文件
        tipColor.material.color = IsOn ? onColor : offColor;
    }

    protected override void SwitchOn()
    {
        base.SwitchOn();
        tipColor.material.DOColor(onColor, 0.5f);
    }

    protected override void SwitchOff()
    {
        base.SwitchOff();
        tipColor.material.DOColor(offColor, 0.5f);
    }
    #endregion

    public void Move(Dir dir)
    {
        int offset = dir.Value();

        if (currentIndex + offset >= top + 1 || currentIndex + offset <= bottom - 1 || !IsOn)
            return;

        currentIndex += offset;

        transform
            .DOBlendableLocalMoveBy(moveAxis * offset, 0.25f)
            .SetEase(Ease.OutSine)
            .PushToTweenPool(EventManager.Instance.MapUpdate);
    }
    
}
