using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class UISettings
{
    [ReadOnly, OdinSerialize] public List<PanelConfigInfo> panelInfos = new();
    [Range(5,10)] public int layerIndexMax = 5;
    
    public UISettings(){}
    
    public UISettings(List<PanelConfigInfo> panelInfoDic)
    {
        this.panelInfos = panelInfoDic;
    }
    
}

