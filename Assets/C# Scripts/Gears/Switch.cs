using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    private bool isOn;
    public bool IsOn
    {
        get { return isOn; }
        set
        {
            if (isOn != value)
            {
                if(value)
                    switchOn?.Invoke();
                else
                    switchOff?.Invoke();    
                isOn = value;
            }
        }
    }

    public UnityAction switchOn;
    public UnityAction switchOff;

    protected virtual void SwitchOn()
    {
        IsOn = true;
    }

    protected virtual void SwitchOff() 
    {
        IsOn = false;
    }
}
