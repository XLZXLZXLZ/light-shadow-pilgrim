using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu(menuName = "SYFramework/SYEditorSettings", fileName = "SYEditorSettings")]
public class SYEditorSettings : SerializedScriptableObject
{
    [OdinSerialize, NonSerialized] public string syRootPath;
    
    /// <summary>
    /// 增加预处理指令
    /// </summary>
    public static void AddScriptCompilationSymbol(string name)
    {
        BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        string group = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
        if (!group.Contains(name))
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, group + ";" + name);
        }
    }

    /// <summary>
    /// 移除预处理指令
    /// </summary>
    public static void RemoveScriptCompilationSymbol(string name)
    {
        BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        string group = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
        if (group.Contains(name))
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, group.Replace(";" + name, string.Empty));
        }
    }
}

#endif



