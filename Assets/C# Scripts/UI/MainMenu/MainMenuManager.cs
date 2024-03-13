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

    public void SwitchChapter(int index)
    {
        stateMachine.SwitchState(stateDic[index]);
    }

    public void ChooseLevelItem(LevelItem levelItem)
    {
        Cover.Instance.ChangeScene("Level" + levelItem.LevelIndex,2);
    }

    public void SetLightDirection(Vector3 direction)
    {
        globalLightTransform.DOLocalRotate(direction, Consts.MainMenuTransformDuration);
    }

    public void SetLightIntensity(float intensity)
    {
        DOTween.To(
            () => globalLight.intensity,
            intensity => globalLight.intensity = intensity,
            intensity,
            Consts.MainMenuChapterDuration);
    }

    public void RotateLight(float angle)
    {
        globalLightTransform.DOLocalRotate(new Vector3(globalLightStartRotation.x, angle, 0), Consts.MainMenuTransformDuration);
    }

    public void SetCameraPos(Vector3 pos)
    {
        cameraTransform.DOLocalMove(pos, Consts.MainMenuChapterDuration);
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
