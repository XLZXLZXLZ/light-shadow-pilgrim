using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using System.Reflection;
#if UNITY_EDITOR
    using UnityEditor;
#endif

public class SYRoot : SerializedMonoSingleton<SYRoot>
{
    public SYInitModule initModule;
    [OdinSerialize] private SYRuntimeSettings runtimeSettings;
    // private Dictionary<Type, PanelConfigInfo> PanelInfoDic => runtimeSettings.uiSettings.panelInfoDic;
    
    public SYRuntimeSettings GetRuntimeSettings()
    {
        // runtimeSettings.uiSettings = new(panelInfoDic);
        return runtimeSettings;
    }
    
#if UNITY_EDITOR

    public static SYEditorSettings GetEditorSettingsInEditorMode()
    {
        return AssetDatabase.LoadAssetAtPath<SYEditorSettings>(SYConstants.EditorSettingPath);
    }

    public static SYRuntimeSettings GetRuntimeSettingsInEditorMode()
    {
        return AssetDatabase.LoadAssetAtPath<SYRuntimeSettings>(SYConstants.RuntimeSettingPath);
    }
    
#endif
}

