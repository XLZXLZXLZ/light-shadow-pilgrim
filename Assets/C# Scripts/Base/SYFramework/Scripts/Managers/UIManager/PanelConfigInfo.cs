using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class PanelConfigInfo
{
    public Type panelType;
    public string assetPath;
    public int layerIndex;
    public bool isShowAndHideDirectly;
    public bool isControlledByLayer;
    public KeyCode showKeyCode;
    public KeyCode hideKeyCode;


#if UNITY_EDITOR
    
    public PanelConfigInfo(
        Type type,
        string assetPath,
        int layerIndex,
        bool isShowAndHideDirectly,
        bool isControlledByLayer,
        KeyCode showKeyCode,
        KeyCode hideKeyCode)
    {
        this.panelType = type;
        this.assetPath = assetPath;
        this.layerIndex = layerIndex;
        this.isShowAndHideDirectly = isShowAndHideDirectly;
        this.isControlledByLayer = isControlledByLayer;
        this.showKeyCode = showKeyCode;
        this.hideKeyCode = hideKeyCode;
    }
    
#endif
    
}

