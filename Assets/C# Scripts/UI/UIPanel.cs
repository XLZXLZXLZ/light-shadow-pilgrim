using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using MyExtensions;
using System.Linq;
using UnityEngine.Events;

public class UIPanel : MonoBehaviour
{
    [SerializeField]
    private float delay = 0.05f;

    [SerializeField]
    protected Graphic[] graphics;
    private Color[] initialColors;

    public UnityAction StartWork;
    public UnityAction EndWork;

    private bool isAnimating;

    private void Awake()
    {
        for(int i = 0; i<transform.childCount;i++)
            transform.GetChild(i).gameObject.SetActive(true);

        graphics = GetComponentsInChildren<Graphic>();
        graphics = graphics.Where(g => !g.TryGetComponent<IIgnoreUISearch>(out var _)).ToArray();
        initialColors = new Color[graphics.Length];
        for (int i = 0; i < graphics.Length; i++)
        {
            initialColors[i] = graphics[i].color;
            graphics[i].color = initialColors[i].GetTransparent();
        }
    }

    public virtual void Show()
    {
        if (isAnimating) 
            return;

        isAnimating = true;
        var sequence = DOTween.Sequence();
        for(int i = 0; i < graphics.Length; i++) 
        {
            sequence.AppendInterval(delay).SetUpdate(true);
            sequence.Join(graphics[i].DOColor(initialColors[i], delay * 2)).SetUpdate(true);
        }
        sequence.OnComplete(ShowComplete);
        StartWork?.Invoke();
    }

    protected virtual void ShowComplete()
    {
        isAnimating = false;
    }

    public virtual void Hide()
    {
        if (isAnimating)
            return;

        isAnimating = true;
        var sequence = DOTween.Sequence();
        for (int i = graphics.Length - 1; i >= 0; i--)
        {
            sequence.AppendInterval(delay).SetUpdate(true);
            sequence.Join(graphics[i].DOColor(initialColors[i].GetTransparent(), delay * 2).SetUpdate(true));
        }
        sequence.OnComplete(HideComplete);
        
    }

    protected virtual void HideComplete()
    {
        isAnimating = false;
        EndWork?.Invoke();
    }
}
