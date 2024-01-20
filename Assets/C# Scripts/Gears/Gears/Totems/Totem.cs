using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SliderType
{
    Loop, //用于循环旋转光线的图腾
    PingPong //用于乒乓旋转光线的图腾
}
public class Totem : Gear,IInteractable
{
    [SerializeField]
    protected Vector3[] sliderPos;
    [SerializeField]
    protected SliderType sliderType;
    [SerializeField]
    protected Transform sliderTransform;
    [SerializeField]
    protected Renderer tipColor;
    [SerializeField]
    protected Color onColor, offColor;
    [SerializeField]
    private bool reverse; //反转乒乓开始位置

    protected int currentIndex; //记录当前滑块位置处于哪个地方

    protected int pingPongModify = 1; //处于乒乓模式时，进行修正

    private void Start()
    {
        tipColor.material = new Material(tipColor.material); //创建临时材质，避免直接替换文件
        tipColor.material.color = IsOn ? onColor : offColor;

        if (reverse)
        {
            currentIndex = sliderPos.Length - 1;
            pingPongModify = -1;
        }

        sliderTransform.position = transform.position + sliderPos[currentIndex];
    }

    protected override void SwitchOn()
    {
        base.SwitchOn();
        tipColor.material.DOColor(onColor, 0.5f);
    }

    protected override void SwitchOff()
    {
        base.SwitchOff();
        tipColor.material.DOColor(offColor, 0.5f);
    }

    private void OnMouseDown()
    {
        if (Time.timeScale == 0)
            return; //暂停时不执行
        OnInteract();
    }

    public virtual void OnInteract() 
    {
    
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        foreach (var pos in sliderPos)
            Gizmos.DrawWireSphere(transform.position + pos, 0.1f);
    }


    //获取滑块的下一个位置，疑似屎山
    protected Vector3 GetNextIndex()
    {
        int maxIndex = sliderPos.Length - 1;

        if (sliderType == SliderType.Loop)
        {
            if (currentIndex == maxIndex)
                currentIndex = 0;
            else
                currentIndex++;
        }
        else if (sliderType == SliderType.PingPong)
        {
            currentIndex += pingPongModify;
            if (currentIndex == maxIndex || currentIndex == 0)
                pingPongModify = -pingPongModify;
        }

        return sliderPos[currentIndex];
    }
}
