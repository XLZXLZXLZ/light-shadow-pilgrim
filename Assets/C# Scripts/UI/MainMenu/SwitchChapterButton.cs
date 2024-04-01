using System;
using System.Collections.Generic;
using UnityEngine;

public class SwitchChapterButton : MonoBehaviour
{
    [SerializeField] private int chapterIndex;

    private void OnMouseUp()
    {
        MainMenuManager.Instance.SwitchChapter(chapterIndex);
    }
}

