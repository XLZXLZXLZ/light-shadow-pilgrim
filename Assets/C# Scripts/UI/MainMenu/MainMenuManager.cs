using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class MainMenuManager : Singleton<MainMenuManager>
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform globalLightTransform;
    [SerializeField] private Light globalLight;
    [SerializeField] private MainMenuStateMachine stateMachine;
    private Dictionary<int, Type> stateDic = new();
    
    private bool isWorking;
    private Vector3 globalLightStartRotation;
    private void Start()
    {
        globalLightStartRotation = globalLightTransform.rotation.eulerAngles;
        stateDic = new()
        {
            {0, typeof(MainMenuChapter0State) },
            {1, typeof(MainMenuChapter1State) },
            {2, typeof(MainMenuChapter2State) },
        };
    }

    /// <summary>
    /// 切换选关界面
    /// </summary>
    public void SwitchChapter(int index)
    {
        stateMachine.SwitchState(stateDic[index]);
    }

    /// <summary>
    /// 选择关卡时
    /// </summary>
    public void ChooseLevelItem(LevelItem levelItem)
    {
        Cover.Instance.ChangeScene("Level" + levelItem.LevelIndex,2);
    }

    /// <summary>
    /// 设置光照方向
    /// </summary>
    /// <param name="direction"></param>
    public void SetLightDirection(Vector3 direction, float duration)
    {
        globalLightTransform.DOLocalRotate(direction, duration);
    }

    /// <summary>
    /// 设置光照强度
    /// </summary>
    /// <param name="intensity"></param>
    public void SetGlobalLightIntensity(float intensity, float duration)
    {
        DOTween.To(
            () => globalLight.intensity,
            intensity => globalLight.intensity = intensity,
            intensity,
            duration);
    }

    /// <summary>
    /// 设置环境光照颜色
    /// </summary>
    /// <param name="lightColor"></param>
    public void SetEnvironmentLightColor(Color32 lightColor, float duration)
    {
        DOTween.To(
            () => RenderSettings.ambientSkyColor,
            color => RenderSettings.ambientSkyColor = color,
            lightColor,
            duration);
    }

    /// <summary>
    /// 旋转光照角度
    /// </summary>
    /// <param name="angle"></param>
    public void RotateLight(float angle, float duration)
    {
        globalLightTransform.DOLocalRotate(
            new Vector3(globalLightStartRotation.x, angle, 0), 
            duration);
    }

    /// <summary>
    /// 设置相机位置
    /// </summary>
    /// <param name="pos"></param>
    public void SetCameraPos(Vector3 pos, float duration)
    {
        cameraTransform.DOLocalMove(pos, duration);
    }

    public void StartWork()
    {
        stateMachine.Begin<MainMenuChapter0State>();
    }
    
    // public void StartWork(bool withAnim)
    // {
    //     if (withAnim)
    //         Camera.main.transform.DOMove(CameraPos, 3f).SetEase(Ease.InOutQuad).OnComplete(() => isWorking = true);
    //     else
    //     {
    //         Camera.main.transform.position = CameraPos;
    //         DOTween.Sequence().AppendInterval(1f).OnComplete(() => isWorking = true);
    //     }
    // }

    // private void Update()
    // {
    //     if (!isWorking)
    //         return;
    // }
}
