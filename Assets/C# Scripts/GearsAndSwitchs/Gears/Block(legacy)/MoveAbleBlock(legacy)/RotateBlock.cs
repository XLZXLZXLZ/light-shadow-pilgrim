using DG.Tweening;
using UnityEngine;

public class RotateBlock : Gear, IInteractable
{
    [SerializeField] private float angle = 90;
    [SerializeField] private Vector3 rotateAxis = Vector3.up;
    [SerializeField] private GearType type = GearType.Loop;
    [SerializeField] private int maxIndex = 3;

    [SerializeField]protected Renderer tipColor;
    [SerializeField]protected Color onColor, offColor;
    private int currentIndex = 0;
    private int pingPongModify = 1;

    #region 机关表现
    private void Start()
    {
        tipColor.material = new Material(tipColor.material); //创建临时材质，避免直接替换文件
        tipColor.material.color = IsOn ? onColor : offColor;
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
    #endregion

    public void OnInteract()
    {
        if(!IsOn) 
            return;

        if (type == GearType.Loop)
        {
            if (currentIndex == maxIndex)
                currentIndex = 0;
            else
                currentIndex++;
        }
        else if (type == GearType.PingPong)
        {
            currentIndex += pingPongModify;
            if (currentIndex == maxIndex || currentIndex == 0)
                pingPongModify = -pingPongModify;
        }

        transform
            .DORotate(rotateAxis * angle * currentIndex, 0.5f)
            .SetEase(Ease.OutQuad)
            .PushToTweenPool(EventManager.Instance.MapUpdate);
        
        AudioManager.Instance.PlaySe(AudioName.ClickNode);
    }

    private void OnMouseDown()
    {
        OnInteract();
    }
}
