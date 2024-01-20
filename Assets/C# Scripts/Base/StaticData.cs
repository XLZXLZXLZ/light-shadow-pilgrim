using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelRecord
{
    private bool[] levelCompleted;
    public ref bool[] LevelCompleted
    {
        get { return ref levelCompleted; }
    }

    public LevelRecord(int count) 
    {
        levelCompleted = new bool[count];
        LoadResult();
    }

    public void SaveResult()
    {
        for (int i = 0; i < levelCompleted.Length; i++)
        {
            PlayerPrefs.SetInt("LevelCompleted" + i, levelCompleted[i] ? 1 : 0);
        }
    }

    public void LoadResult()
    {
        for (int i = 0; i < levelCompleted.Length; i++)
        {
            levelCompleted[i] = PlayerPrefs.GetInt("LevelCompleted" + i) == 1;
        }
    }
}
public static class StaticData
{
    private static LevelRecord levelRecord = new(8);
    public static bool[] LevelCompleted => levelRecord.LevelCompleted;

    public static void CompleteLevel(int index)
    {
        index -= 1;
        levelRecord.LevelCompleted[index] = true;
        levelRecord.SaveResult();
    }
}
