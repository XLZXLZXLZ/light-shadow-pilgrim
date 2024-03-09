using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TransmitSwitch : Switch,ITriggerable
{
    [SerializeField] private TransmitSwitch target;
    // private Animator anim;
    public PlatformNode PlatformNode { get; private set; }

    private void Start()
    {
        // anim = GetComponent<Animator>();
        PlatformNode = GetComponent<PlatformNode>();
    }

    public void OnTrigger()
    {
        SwitchOn();
        
        // 在这里没有什么表现画面，仅仅是想触发这个事件且保证不会多次触发Start
        DOTween.Sequence().PushToTweenPool(EventManager.Instance.Transmit);
        EventManager.Instance.OnForceToSetNodeByTransmit.Invoke(target);

        // anim.Play("SwitchOn");
    }

    public void OnTriggerOver()
    {
        SwitchOff();
        // anim.Play("SwitchOff");
    }
}

