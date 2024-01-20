using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gear : MonoBehaviour
{
    [SerializeField]
    private Switch[] targetSwitch;
    protected bool IsOn => targetSwitch.Length == 0 || applyCount > 0; //若未为其标记开关，我们默认它是开启的
    private int applyCount = 0; //接受的正在激活的开关数量，当连接多开关时，仅需存在一个开启的开关即可 

    protected virtual void Awake()
    {
        if (targetSwitch == null) 
            return;
        foreach (Switch sw in targetSwitch)
        {
            sw.switchOn += SingleSwitchOn;
            sw.switchOff += SingleSwitchOff;
        }
    }

    private void SingleSwitchOn()
    {
        applyCount++;
        SwitchOn();
    }

    private void SingleSwitchOff()
    {
        applyCount--;
        if(applyCount == 0)
            SwitchOff();
    }

    protected virtual void SwitchOn()
    {

    }

    protected virtual void SwitchOff()
    {

    }

    protected virtual void OnDrawGizmos()
    {
        if(targetSwitch.Length == 0) 
            return;

        Gizmos.color = Color.red;
        foreach (Switch sw in targetSwitch)
        {
            Gizmos.DrawLine(transform.position, sw.transform.position);
            Gizmos.DrawWireSphere(sw.transform.position, 0.5f);
        }
    }
}
