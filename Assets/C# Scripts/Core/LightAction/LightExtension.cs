using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

//当一个物体可以被光影影响时，挂载该脚本以判断当前状态
public class LightExtension : MonoBehaviour
{
    [SerializeField] private LightState lightState;
    public LightState LightState
    {
        get {  return lightState; }
        private set
        {
            if (value != lightState)
            {
                lightState = value; //新光照信息不统一时，更新光照信息

                if (value == LightState.Light)
                    OnLighted?.Invoke();
                else
                    OnDarken?.Invoke();
            }
        }
    }

    public UnityAction OnLighted;
    public UnityAction OnDarken;

    private void Awake()
    {
        EventManager.Instance.OnMapUpdateFinished += OnStateUpdate;
    }

    // private void Update()
    // {
    //     OnStateUpdate();
    // }

    //向光线方向投射射线，若未与地形碰撞则代表该地块为亮
    public void OnStateUpdate()
    {
        Ray ray = new Ray(transform.position,-GlobalLight.Instance.LightDirInLogic);
        bool isCovered = Physics.Raycast(ray, 100, LayerMask.GetMask("Ground"));

        if (isCovered)
            LightState = LightState.Dark;
        else
            LightState = LightState.Light;
    }

    private void OnDrawGizmos()
    {
        OnStateUpdate();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = LightState == LightState.Dark ? Color.red : Color.green;
        Gizmos.DrawLine(transform.position, transform.position - GlobalLight.Instance.LightDirInLogic * 10);
    }
}
