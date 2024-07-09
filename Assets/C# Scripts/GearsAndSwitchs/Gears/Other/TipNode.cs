using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipNode :MonoBehaviour, ITriggerable
{
    [SerializeField]
    private string tipWord;

    [SerializeField]
    private bool androidSpecial = false;

    [SerializeField]
    private string androidTipWord = "";

    public void OnTrigger()
    {
        var tip = tipWord;

#if UNITY_ANDROID
        if(androidSpecial)
            tip = androidTipWord;
#endif

        UIManager.Instance.GetPanelAsync<TipWordPanel>(
            tipWordPanel => tipWordPanel.ShowTipWord(tip));
        Destroy(this);
    }

    public void OnTriggerOver()
    {

    }
}
