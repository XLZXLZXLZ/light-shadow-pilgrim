using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class RotateBlock : Gear, IInteractable
{
    [SerializeField] private float angle = 90;
    [SerializeField] private Vector3 rotateAxis = Vector3.up;
    [SerializeField] private GearType type = GearType.Loop;
    [SerializeField] private int maxIndex = 3;
    private int currentIndex = 0;
    private int pingPongModify = 1;

    public void OnInteract()
    {
        if(!IsOn) 
            return;

        if (type == GearType.Loop)
        {
            if (currentIndex == maxIndex)
                currentIndex = 0;
            else
                currentIndex++;
        }
        else if (type == GearType.PingPong)
        {
            currentIndex += pingPongModify;
            if (currentIndex == maxIndex || currentIndex == 0)
                pingPongModify = -pingPongModify;
        }

        transform.DORotate(rotateAxis * angle * currentIndex, 0.5f).SetEase(Ease.OutQuad);
    }

    private void OnMouseDown()
    {
        OnInteract();
    }
}
