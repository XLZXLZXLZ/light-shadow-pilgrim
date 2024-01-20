using DG.Tweening;
using MyExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private Image outLine;
    private Button button;
    private UIPanel parent;

    private bool isWorking = false;

    private void Awake()
    {
        button = GetComponent<Button>();

        parent = GetComponentInParent<UIPanel>();
        parent.StartWork += StartWork;
        parent.EndWork += EndWork;

        if (outLine != null) return;
        outLine = transform.GetComponentInChildren<OutLine>().GetComponent<Image>(); //极其诡异的代码
        if (outLine != null) return;
        Debug.LogError("错误，未找到按钮组件的outLine特效");
    }

    private void Start()
    {
        button.interactable = false;

        if (outLine == null) return;
        outLine.color = new Color(1, 1, 1, 0);
    }

    private void StartWork()
    {
        isWorking = true;
        button.interactable = true;
    }

    private void EndWork()
    {
        isWorking = false;
        outLine.DOColor(Color.white.GetTransparent(), 0.25f).SetUpdate(true);
        button.interactable = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isWorking) return;
        outLine.DOColor(Color.white, 0.25f).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isWorking) return;
        outLine.DOColor(Color.white.GetTransparent(), 0.25f).SetUpdate(true);
    }
}
