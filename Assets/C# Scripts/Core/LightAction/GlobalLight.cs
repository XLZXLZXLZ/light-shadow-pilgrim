using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GlobalLight : Singleton<GlobalLight>
{
    private static float[] heightMap = { 18.434f, 26.565f, 45f, 56.310f, 63.435f ,90f}; //每级高度的arctan值，对应1/3,1/2,1,3/2,2,垂直

    [SerializeField]
    private int defaultLevel = 2; //默认光线高度等级(45度)
    private int currentLevel = 2; //当前高度等级（逻辑上）

    private Vector3 lightAngleInLogic;
    public Vector3 LightDirInLogic => Quaternion.Euler(lightAngleInLogic) * Vector3.forward;//逻辑光线方向
    public Vector3 LightDirInView =>  transform.rotation * Vector3.forward; //表现光线方向

    private bool canChange = true; //标记当前整体光线是否可旋转

    private void Start()
    {
        // 初始化时，当前高度等级和光照方向按照设置好的值
        currentLevel = defaultLevel;
        lightAngleInLogic = transform.eulerAngles;
        //LightDirInLogic = LightDirInView;
    }
    
    /// <summary>
    /// 请求旋转全局光线,顺时针为正
    /// </summary>
    /// <param name="rotateAngle">旋转角</param>
    /// <returns></returns> 
    public bool Rotate(float rotateAngle) 
    {
        if (!canChange)
            return false;
        canChange = false;
        
        // 此时光照逻辑更新刚开始，
        EventManager.Instance.OnMapUpdateStart.Invoke();

        // 先更新光照方向实际值
        lightAngleInLogic += Vector3.up * rotateAngle;
        
        // 再更新光照方向表现
        transform.DOLocalRotate(transform.eulerAngles + new Vector3(0, rotateAngle, 0), 0.6f)
            .SetEase(Ease.OutQuad)//旋转地图，并在旋转完毕后通知更新
            .OnComplete(() =>
            {
                canChange = true;
                EventManager.Instance.OnMapUpdateFinished.Invoke(); //更新地图
            });
        
        // 限制玩家移动
        // Player.Instance.InterruptMovement(0.5f);
        
        return true;
    }

    /// <summary>
    /// 请求修改全局光线高度，dir为修改量
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public bool ChangeHeight(int dir)
    {
        if (!canChange)
            return false;

        if (currentLevel + dir < 0 || currentLevel + dir >= heightMap.Length)
            return false;
        
        EventManager.Instance.OnMapUpdateStart.Invoke();
        
        currentLevel += dir;
        canChange = false;
        
        lightAngleInLogic.x = heightMap[currentLevel];
        transform.DOLocalRotate(new Vector3(heightMap[currentLevel], transform.eulerAngles.y, 0), 0.6f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                canChange = true;
                EventManager.Instance.OnMapUpdateFinished.Invoke(); //更新地图
            });
        
        // Player.Instance.InterruptMovement(0.5f);
        return true;

    }
}
