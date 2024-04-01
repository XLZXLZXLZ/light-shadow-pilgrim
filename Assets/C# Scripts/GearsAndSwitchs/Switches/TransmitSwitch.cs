using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TransmitSwitch : Switch,ITriggerable,IRepeatTriggerable
{
    [SerializeField] private TransmitSwitch target;
    [SerializeField] private GameObject transmitParticle;
    // private Animator anim;
    public PlatformNode PlatformNode { get; private set; }

    private void Start()
    {
        // anim = GetComponent<Animator>();
        PlatformNode = GetComponent<PlatformNode>();
    }

    public void OnTrigger()
    {
        if (!target.PlatformNode.ReachAble(GameManager.Instance.CurrentPlayerState)) return;
        
        SwitchOn();
        
        // 在这里没有什么表现画面，仅仅是想触发这个事件且保证不会多次触发Start
        DOTween.Sequence().PushToTweenPool(EventManager.Instance.Transmit);
        EventManager.Instance.OnForceToSetNodeByTransmit.Invoke(target);

        target.Effect();
        Effect();

        // anim.Play("SwitchOn");
    }

    public void OnTriggerOver()
    {
        SwitchOff();
        // anim.Play("SwitchOff");
    }

    public void Effect()
    {
        Instantiate(transmitParticle, transform.position, Quaternion.identity);
    }

    public void RepeatTrigger()
    {
        OnTrigger();
    }
}

