using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BlockProGroup : Gear
{
    [SerializeField] private List<BlockProBase> blocks = new();
    [SerializeField] private float interval;

    protected override void SwitchOn()
    {
        base.SwitchOn();

        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < blocks.Count; i++)
        {
            if (i != 0)
                sequence.AppendInterval(interval);
            Action action = blocks[i].SwitchOn;
            sequence.AppendCallback(() => action.Invoke());
        }
    }

    protected override void SwitchOff()
    {
        base.SwitchOff();
        
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < blocks.Count; i++)
        {
            if (i != 0)
                sequence.AppendInterval(interval);
            Action action = blocks[i].SwitchOff;
            sequence.AppendCallback(() => action.Invoke());
        }
    }
}

