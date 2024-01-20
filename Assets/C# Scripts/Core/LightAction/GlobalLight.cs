using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GlobalLight : Singleton<GlobalLight>
{
    private static float[] heightMap = { 18.434f, 26.565f, 45f, 56.310f, 63.435f ,90f}; //每级高度的arctan值，对应1/3,1/2,1,3/2,2,垂直

    [SerializeField]
    private int defaultLevel = 2; //默认光线高度等级(45度)
    private int currentLevel = 2; //当前高度等级

    public Vector3 LightDir => transform.rotation * Vector3.forward; //光线方向

    private bool canChange = true; //标记当前整体光线是否可旋转

    private void Start()
    {
        currentLevel = defaultLevel;
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
        transform.DOLocalRotate(transform.eulerAngles + new Vector3(0, rotateAngle, 0), 0.6f)
            .SetEase(Ease.OutQuad)//旋转地图，并在旋转完毕后通知更新
            .OnComplete(() =>
            {
                canChange = true;
                EventManager.Instance.OnMapUpdate?.Invoke(); //更新地图
            });
        Player.Instance.InterruptMovement(0.5f);
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

        currentLevel += dir;
        canChange = false;
        transform.DOLocalRotate(new Vector3(heightMap[currentLevel], transform.eulerAngles.y, 0), 0.6f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                canChange = true;
                EventManager.Instance.OnMapUpdate?.Invoke(); //更新地图
            });
        Player.Instance.InterruptMovement(0.5f);
        return true;

    }
}
