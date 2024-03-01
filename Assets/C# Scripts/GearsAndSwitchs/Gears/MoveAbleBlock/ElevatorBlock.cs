using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformNode))]
public class ElevatorBlock :MonoBehaviour, ITriggerable
{
    private Elevator parent;

    private void Awake()
    {
        parent = GetComponentInParent<Elevator>();
    }

    public void OnTrigger()
    {
        parent.Work();
    }

    public void OnTriggerOver()
    {
        
    }
}
