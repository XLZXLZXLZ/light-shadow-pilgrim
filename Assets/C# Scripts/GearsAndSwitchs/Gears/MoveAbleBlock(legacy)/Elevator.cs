using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private Vector3 target;
    private Vector3 origin;

    [SerializeField]
    private float duration = 2;

    [SerializeField]
    private Transform gears; //拖入机关物体

    //象征着屎山的三个flag喵
    private bool isWorking; //标记是否正在工作
    private bool isReverse; //标记是否该返回
    private bool isReady = true; //为避免玩家在电梯上换位导致又坠下去，当玩家离开电梯后才允许下一次移动
    

    private void Awake()
    {
        origin = transform.position;
        EventManager.Instance.OnMoveToNewNode += UpdateState;
    }

    public bool Work()
    {
        if (isWorking || !isReady)
            return false;

        transform
            .DOMove(isReverse ? origin : transform.position + target, duration).SetEase(Ease.Linear)
            .OnComplete(() => isWorking = false)
            .PushToTweenPool(EventManager.Instance.MapUpdate);
        
        isReverse = !isReverse;
        isWorking = true;
        isReady = false;
        
        return true;
    }

    private void UpdateState(PathNode node)
    {
        if (isReady) 
            return;

        if (node.transform.parent != transform)
            isReady = true;
    }

    private void Update()
    {
        if (isWorking)
            gears.Rotate(0, 0, Time.deltaTime * 180);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,transform.position + target);
        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }
}
