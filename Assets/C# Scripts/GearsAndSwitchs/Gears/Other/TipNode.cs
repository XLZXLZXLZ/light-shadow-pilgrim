using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipNode :MonoBehaviour, ITriggerable
{
    [SerializeField]
    private string tipWord;

    public void OnTrigger()
    {
        UIManager.Instance.GetPanelAsync<TipWordPanel>(
            tipWordPanel => tipWordPanel.ShowTipWord(tipWord));
        Destroy(this);
    }

    public void OnTriggerOver()
    {

    }
}
