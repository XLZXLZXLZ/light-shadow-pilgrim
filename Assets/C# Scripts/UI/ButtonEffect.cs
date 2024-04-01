using DG.Tweening;
using MyExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private Image outLine;
    // private Color outLineColor;
    private void Start()
    {
        // outLineColor = outLine.color;
        outLine.color = new Color(1, 1, 1, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        outLine.DOColor(Color.white, 0.25f).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outLine.DOColor(Color.white.GetTransparent(), 0.25f).SetUpdate(true);
    }
}
