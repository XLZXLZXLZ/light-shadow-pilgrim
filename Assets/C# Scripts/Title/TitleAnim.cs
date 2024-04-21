using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class TitleAnim : MonoBehaviour
{
    public Action onComplete;

    [SerializeField]
    private Transform lightTitle;

    [SerializeField]
    private Transform blackTitle;

    [SerializeField]
    private Transform globalLight;

    [SerializeField]
    private float startInterval = 2f;

    [SerializeField]
    private float fallInterval = 0.1f;

    [SerializeField]
    private float moveTime = 2f;

    [SerializeField]
    private float shadowInterval = 6f;

    [SerializeField]
    private float shadowAppearTime = 0.5f;

    private void Start() //屎山勿动
    {
        for (int i = 0; i < lightTitle.childCount; i++)
        {
            var child = lightTitle.GetChild(i);
            for (int j = 0; j < child.childCount; j++)
            {
                var t = child.GetChild(j);

                Vector3 origin = t.position;
                float delay = t.position.y * fallInterval + t.position.x * fallInterval + startInterval;
                t.position += Vector3.up * 20;
                delay = Mathf.Max(0, delay);

                DOTween.Sequence()
                    .AppendInterval(delay)
                    .Append(t.DOMove(origin, moveTime).SetEase(Ease.InQuart));
            }
        }

        blackTitle.localScale -= Vector3.forward;
        blackTitle.gameObject.SetActive(false);

        DOTween.Sequence()
            .AppendInterval(shadowInterval)
            .Append(transform.DOMove(transform.position, 0).OnComplete(() => blackTitle.gameObject.SetActive(true))) //神秘代码
            .Append(blackTitle.DOScale(new Vector3(1, 1, 1.5f), shadowAppearTime))
            .Join(globalLight.DORotate(new Vector3(45, 180, 0), shadowAppearTime))
            .OnComplete(()=>onComplete?.Invoke());
    }


}
