
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

//当一个物体可以被光影影响时，挂载该脚本以判断当前状态
public class LightExtension : MonoBehaviour
{
    // 看这个LightExtension会不会在地图更新结束后自动检测当前状态（方便需要强制修改LightState）
    [field: SerializeField] public bool IsAutoDetectLight { get; private set; } = true;
    [SerializeField] private LightState lightState;
    public bool isLightCasted;
    public LightState LightState
    {
        get {  return lightState; } 
        set
        {
            if (value != lightState)
            {
                lightState = value; //新光照信息不统一时，更新光照信息

                if (value == LightState.Dark)
                    OnDarken?.Invoke();
                else
                    OnLighted?.Invoke();
            }
        }
    }

    public UnityAction OnLighted;
    public UnityAction OnDarken;

    private void Awake()
    {
        if (IsAutoDetectLight)
        {
            EventManager.Instance.MapUpdate.OnEarlyStart += OnStateEarlyUpdate;
            EventManager.Instance.MapUpdate.OnFinished += OnStateUpdate;
            EventManager.Instance.OnGenerateMapFinished += OnStateUpdate;
        }


    }

    // private void Update()
    // {
    //     OnStateUpdate();
    // }


    public void OnStateEarlyUpdate()
    {
        isLightCasted = false;
    }
    
    /// <summary>
    ///向光线方向投射射线，若未与地形碰撞则代表该地块为亮
    /// </summary>
    public void OnStateUpdate()
    {
        if (!IsAutoDetectLight) return;
        Ray ray = new Ray(transform.position,-GlobalLight.Instance.LightDirInLogic);
        bool isCovered = Physics.Raycast(ray, 100, LayerMask.GetMask("Ground"));
        if (isLightCasted)
        {
            LightState = LightState.Light;
        }
        else if (isCovered)
        {
            LightState = LightState.Dark;
        }
        else
        {
            LightState = LightState.Light;
        }

        

    }

    private void OnDrawGizmos()
    {
        //OnStateUpdate();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = LightState == LightState.Dark ? Color.red : Color.green;
        Gizmos.DrawLine(transform.position, transform.position - GlobalLight.Instance.LightDirInLogic * 100);

        Ray ray = new Ray(transform.position, -GlobalLight.Instance.LightDirInLogic);
        bool b = Physics.Raycast(ray, out var rayCast, 100, LayerMask.GetMask("Ground"));
        if (!b) return;
    }
}
