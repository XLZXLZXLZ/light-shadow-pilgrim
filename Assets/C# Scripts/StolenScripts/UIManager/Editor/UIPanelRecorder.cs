using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIPanelRecorder
{
    // private const string panelContainerPath = @"Assets/C#Scripts/Managers/UIManager/PanelContainer.asset";
    private const string panelContainerPath = @"Assets/C# Scripts/StolenScripts/UIManager/PanelContainer.asset";
    private static PanelContainer _panelContainer;

    private static PanelContainer panelContainer
    {
        get
        {
            if (_panelContainer == null)
            {
                _panelContainer = AssetDatabase.LoadAssetAtPath<PanelContainer>(panelContainerPath);
                if (_panelContainer == null)
                {
                    _panelContainer = ScriptableObject.CreateInstance<PanelContainer>();
                    AssetDatabase.CreateAsset(_panelContainer, panelContainerPath);
                    EditorUtility.SetDirty(_panelContainer);
                    AssetDatabase.SaveAssets();
                }
            }

            return _panelContainer;
        }
    }
    
    
    [MenuItem("Assets/SavePanel")]
    static void Select()
    {
        string[] guids = Selection.assetGUIDs;
        foreach (var guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);

            GameObject panelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
            if (panelPrefab == null) return;
            PanelBase panelComponent = panelPrefab.GetComponent<PanelBase>();
            if (panelComponent == null) return;
            
            panelContainer.AddPanel(panelComponent);
            EditorUtility.SetDirty(panelContainer);
            AssetDatabase.SaveAssets();
            
        }
    }
    
    [MenuItem("Assets/SavePanel", true)]
    static bool IsSelect()
    {
        return null != Selection.assetGUIDs && Selection.assetGUIDs.Length > 0;
    }
}
