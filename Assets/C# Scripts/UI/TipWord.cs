using DG.Tweening;
using MyExtensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TipWord : Singleton<TipWord>
{
    private TextMeshProUGUI tmp;

    private float timer = Consts.tipWordTimer;

    protected override void Awake()
    {
        base.Awake();
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.color = Color.white.GetTransparent();
    }

    public void UpdateTip(string word)
    {
        DOTween.Sequence()
            .Append(tmp.DOColor(Color.white.GetTransparent(), 0.5f).OnComplete(() => tmp.text = word))
            .Append(tmp.DOColor(Color.white, 0.5f));
        timer = Consts.tipWordTimer;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0) 
        {
            if (tmp.text != "")
                UpdateTip("");
        }
    }
}
