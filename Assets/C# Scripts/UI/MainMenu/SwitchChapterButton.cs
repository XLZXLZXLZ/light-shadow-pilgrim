using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwitchChapterButton : MonoBehaviour
{
    [SerializeField] private int chapterIndex;

    private Vector3 startPos;

    private void Awake()
    {
        startPos = transform.localPosition;
    }

    #region Public


    public void MoveUp()
    {
        transform.DOLocalMove(startPos + Vector3.up * 0.5f, 0.25f);
        AudioManager.Instance.PlaySe(AudioName.MainMenuMouseOver);
    }

    public void MoveDown()
    {
        transform.DOLocalMove(startPos, 0.25f);
    }

    #endregion

    private void OnMouseEnter()
    {
        MoveUp();
    }

    private void OnMouseExit()
    {
        MoveDown();
    }

    private void OnMouseUp()
    {
        MainMenuManager.Instance.SwitchChapter(chapterIndex);
    }
}

