using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipNode :MonoBehaviour, ITriggerable
{
    [SerializeField]
    private string tipWord;

    public void OnTrigger()
    {
        TipWord.Instance.UpdateTip(tipWord);
        Destroy(this);
    }

    public void OnTriggerOver()
    {

    }
}
