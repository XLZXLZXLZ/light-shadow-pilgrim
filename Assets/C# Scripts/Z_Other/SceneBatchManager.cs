/*
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class SceneBatchProcessor : EditorWindow
{
    //private static Color color = new Color(175f/255,249f/255,1);
    //7BBBC0  71A980
    private static Color color = new Color(0.780F, 1, 0.808F);

    [MenuItem("Tools/Process Scenes")]
    public static void ProcessScenes()
    {
        // 获取所有场景
        string[] scenePaths = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .Where(path => path.Contains("Level2"))
            .ToArray();

        foreach (string scenePath in scenePaths)
        {
            // 打开场景
            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);

            // 这里可以添加你的相机和照明设置修改逻辑
            // 例如：修改相机的位置、旋转、照明设置等等

            Camera.main.backgroundColor = color;
            RenderSettings.fogColor = color;

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());


            // 保存修改
            EditorSceneManager.SaveOpenScenes();
        }

        Debug.Log("All scenes processed successfully.");
    }
}
*/
