using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PanelConfigInfo
{
    public Type panelType;
    public string assetPath;
    public int layerIndex;
    public bool isHideDirectly;
    public KeyCode showKeyCode;
    public KeyCode hideKeyCode;


#if UNITY_EDITOR
    
    public PanelConfigInfo(
        Type type,
        string assetPath,
        int layerIndex,
        bool isHideDirectly,
        KeyCode showKeyCode,
        KeyCode hideKeyCode)
    {
        this.panelType = type;
        this.assetPath = assetPath;
        this.layerIndex = layerIndex;
        this.isHideDirectly = isHideDirectly;
        this.showKeyCode = showKeyCode;
        this.hideKeyCode = hideKeyCode;
    }
    
#endif
    
}

