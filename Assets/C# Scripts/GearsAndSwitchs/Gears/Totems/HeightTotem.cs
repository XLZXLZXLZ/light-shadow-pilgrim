using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightTotem :Totem
{
    private bool canMove = true;
    public override void OnInteract()
    {
        if (!IsOn || !canMove) return; //未启用或正在改变动画时，点击无效

        if (!GlobalLight.Instance.ChangeHeight(pingPongModify)) return; //尝试修改光线高度，若不成功，退出函数

        var target = GetNextIndex();

        canMove = false;
        sliderTransform.DOMove(transform.position + sliderPos[currentIndex], 0.6f).OnComplete(() => canMove = true);
        
        AudioManager.Instance.PlaySe(AudioName.ClickNode);
    }
}
