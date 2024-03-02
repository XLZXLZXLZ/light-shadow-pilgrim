using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家踩上去触发的开关
/// </summary>
public class StepSwitch : Switch, ITriggerable
{
    [SerializeField]
    private bool resetable;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnTrigger()
    {
        SwitchOn();
        anim.Play("SwitchOn");
    }

    public void OnTriggerOver()
    {
        if (!resetable)
            return;

        SwitchOff();
        anim.Play("SwitchOff");
    }
}
