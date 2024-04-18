using DG.Tweening;
using MyExtensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleTipWord : MonoBehaviour
{
    [SerializeField]
    private TitleAnim title;

    private bool isShow;

    private void Start()
    {
        var t = GetComponent<TextMeshProUGUI>();
        var c = t.color;

        t.color = c.GetTransparent();
        
        title.onComplete += () =>
            DOTween.Sequence().AppendInterval(2f)
                        .Append(t.DOColor(c, 1f)
                        .OnComplete(() => isShow = true));
    }

    private void Update()
    {
        if (isShow && Input.anyKey) 
        {
            isShow = false;
            Cover.Instance.ChangeScene("MainMenu", 2, 1);
        }
    }
}
