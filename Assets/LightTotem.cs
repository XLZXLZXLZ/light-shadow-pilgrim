using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightTotem : Totem
{
    #region 交互部分
    private bool canMove = true;

    [SerializeField] private CastLight castLight;
    [SerializeField] private Renderer lightRenderer;
    [SerializeField] private Material lightMaterial;
    [SerializeField] private Material normalMaterial;
    [SerializeField]private bool isLightOn = false;
    public override void OnInteract()
    {
        if (!IsOn || !canMove) return; //未启用或正在改变动画时，点击无效

        //  if (!GlobalLight.Instance.Rotate(-90 * pingPongModify)) return; //尝试旋转光线，若不成功，退出函数
        isLightOn = !isLightOn;

        if (isLightOn)
        {
            lightRenderer.material = lightMaterial;
            castLight.AddCastEvent();
            EventManager.Instance.MapUpdate.StartStageEvent();
        }
        else
        {
            
            lightRenderer.material = normalMaterial;
            castLight.DeleteCastEvent();
            EventManager.Instance.MapUpdate.StartStageEvent();
        }
        var target = GetNextIndex();
        
        canMove = false;
        sliderTransform.DOMove(transform.position + sliderPos[currentIndex], 0.6f).OnComplete(() => canMove = true);
    }
    #endregion

}