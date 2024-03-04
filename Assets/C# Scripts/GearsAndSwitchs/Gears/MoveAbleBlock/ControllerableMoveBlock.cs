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

    private int currentIndex = 0;

    public void Move(Dir dir)
    {
        int offset = dir.Value();

        if (currentIndex + offset >= top + 1 || currentIndex + offset <= bottom - 1 || !IsOn)
            return;

        currentIndex += offset;

        transform
            .DOBlendableMoveBy(moveAxis * offset, 0.25f)
            .SetEase(Ease.OutSine)
            .PushToTweenPool(EventManager.Instance.MapUpdate);
    }
    
}
