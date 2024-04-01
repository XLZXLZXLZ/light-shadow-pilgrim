using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SYConstants
{
    public const string RootPath = "Assets/C# Scripts/Base/SYFramework/Configs";
    public const string EditorSettingAssetName = "SYEditorSettings.asset";
    public const string RuntimeSettingAssetName = "SYRuntimeSettings.asset";

    public const int UILayerMaxCount = 5;

    public static string EditorSettingPath => Path.Combine(RootPath,EditorSettingAssetName);
    public static string RuntimeSettingPath => Path.Combine(RootPath,RuntimeSettingAssetName);
    
}

