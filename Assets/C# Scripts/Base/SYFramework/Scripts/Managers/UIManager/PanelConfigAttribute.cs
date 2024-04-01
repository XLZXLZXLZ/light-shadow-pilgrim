using System;
using System.Collections.Generic;
using UnityEngine;


[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class PanelConfigAttribute : Attribute
{
    public Type type;
    public string assetPath;
    public int layerIndex;
    public bool isHideDirectly;
    public KeyCode showKeyCode;
    public KeyCode hideKeyCode;
    
    public PanelConfigAttribute(
        Type panelType,
        string panelAssetPath,
        int panelLayerIndex,
        bool isHideDirectly,
        KeyCode showKeyCode,
        KeyCode hideKeyCode)
    {
        this.type = panelType;
        this.assetPath = panelAssetPath;
        this.layerIndex = Mathf.Clamp(panelLayerIndex,0, SYConstants.UILayerMaxCount);
        this.isHideDirectly = isHideDirectly;
        this.showKeyCode = showKeyCode;
        this.hideKeyCode = hideKeyCode;
    }
    
    public PanelConfigAttribute(
        Type panelType,
        string panelAssetPath,
        int panelLayerIndex,
        bool isHideDirectly)
    {
        this.type = panelType;
        this.assetPath = panelAssetPath;
        this.layerIndex = Mathf.Clamp(panelLayerIndex,0, SYConstants.UILayerMaxCount);
        this.isHideDirectly = isHideDirectly;
        this.showKeyCode = KeyCode.None;
        this.hideKeyCode = KeyCode.None;
    }
    
    public PanelConfigAttribute(Type panelType,int panelLayerIndex)
    {
        this.type = panelType;
        this.assetPath = panelType.FullName;
        this.layerIndex = Mathf.Clamp(panelLayerIndex,0, SYConstants.UILayerMaxCount);
        isHideDirectly = false;
        showKeyCode = KeyCode.None;
        showKeyCode = KeyCode.None;
    }
}

