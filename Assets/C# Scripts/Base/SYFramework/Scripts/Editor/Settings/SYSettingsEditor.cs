using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using UnityEngine.Serialization;

public class SYSettingsEditor : OdinEditor
{
    public static SYEditorSettings editorSettings;
    public static SYEditorSettings EditorSettings
    {
        get
        {
            if (editorSettings == null)
                editorSettings = AssetDatabase.LoadAssetAtPath<SYEditorSettings>(SYConstants.EditorSettingPath);
            return editorSettings;
        }
    }
    public static SYRuntimeSettings runtimeSettings;
    
    public static SYRuntimeSettings RuntimeSettings
    {
        get
        {
            if (runtimeSettings == null)
                runtimeSettings = AssetDatabase.LoadAssetAtPath<SYRuntimeSettings>(SYConstants.RuntimeSettingPath);
            return runtimeSettings;
        }
    }

    [InitializeOnLoadMethod]
    public static void RefreshSettings()
    {
        RefreshRuntimeSettings();
    }

    private static void RefreshRuntimeSettings()
    {
        // 如果当前正在PlayMode就不刷新
        if (EditorApplication.isPlayingOrWillChangePlaymode) return;
        
        // 进行刷新前必须有一个EditorSettings
        if (EditorSettings == null || RuntimeSettings == null)
        {
            SYLog.LogError($"SYEditorSettingsEditor：更新前请创建一个EditorSettings和一个RuntimeSettings到{SYConstants.RootPath}下");
            return;
        }

        RefreshUISettingsInEditor();
    }
    
    private static void RefreshUISettingsInEditor()
    {
        RuntimeSettings.uiSettings.panelInfos.Clear();
        // 获取所有程序集
        System.Reflection.Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        Type baseType = typeof(PanelBase);
        // 遍历程序集
        foreach (System.Reflection.Assembly assembly in assemblies)
        {

            // 遍历程序集下的每一个类型
            try
            {
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (baseType.IsAssignableFrom(type)
                        && !type.IsAbstract)
                    {
                        var attributes = type.GetCustomAttributes<PanelConfigAttribute>();
                        foreach (var attribute in attributes)
                        {
                            RuntimeSettings.uiSettings.panelInfos.Add(
                                new PanelConfigInfo(
                                    attribute.type,
                                    attribute.assetPath,
                                    attribute.layerIndex,
                                    attribute.isShowAndHideDirectly,
                                    attribute.isControlledByLayer,
                                    attribute.showKeyCode,
                                    attribute.hideKeyCode));
                        }

                    }
                }
            }
            
            catch (Exception)
            {
                continue;
            }
        }
    }
}
