using System;
using System.Collections.Generic;
using UnityEngine;


[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class PanelConfigAttribute : Attribute
{
    public Type type;                   // 该Panel的类型（继承PanelBase的类型）
    public string assetPath;            // 加载该Panel的路径
    public int layerIndex;              // 
    public bool isShowAndHideDirectly;
    public bool isControlledByLayer;
    public KeyCode showKeyCode;
    public KeyCode hideKeyCode;
    
    public PanelConfigAttribute(
        Type panelType,
        string panelAssetPath,
        int panelLayerIndex,
        bool isShowAndHideDirectly,
        bool isControlledByLayer,
        KeyCode showKeyCode,
        KeyCode hideKeyCode)
    {
        this.type = panelType;
        this.assetPath = panelAssetPath;
        this.layerIndex = Mathf.Clamp(panelLayerIndex,0, SYConstants.UILayerMaxCount);
        this.isShowAndHideDirectly = isShowAndHideDirectly;
        this.isControlledByLayer = isControlledByLayer;
        this.showKeyCode = showKeyCode;
        this.hideKeyCode = hideKeyCode;
    }
    
    public PanelConfigAttribute(
        Type panelType,
        string panelAssetPath,
        int panelLayerIndex,
        bool isShowAndHideDirectly,
        bool isControlledByLayer)
    {
        this.type = panelType;
        this.assetPath = panelAssetPath;
        this.layerIndex = Mathf.Clamp(panelLayerIndex,0, SYConstants.UILayerMaxCount);
        this.isShowAndHideDirectly = isShowAndHideDirectly;
        this.isControlledByLayer = isControlledByLayer;
        this.showKeyCode = KeyCode.None;
        this.hideKeyCode = KeyCode.None;
    }
}

