using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenuManager : Singleton<MainMenuManager>
{
    [SerializeField] private Vector3 CameraPos;
    [SerializeField] private Transform globalLight;
    [SerializeField] private MainMenuStateMachine stateMachine;
    
    private bool isWorking;
    private Vector3 globalLightStartRotation;
    private void Start()
    {
        globalLightStartRotation = globalLight.rotation.eulerAngles;
    }

    public void SwitchChapter<T>() where T : MainMenuStateBase
    {
        stateMachine.SwitchState<T>();
    }

    public void ChooseLevelItem(LevelItem levelItem)
    {
        Cover.Instance.ChangeScene("Level" + levelItem.LevelIndex,2);
    }

    public void RotateLight(float angle)
    {
        globalLight.DOLocalRotate(new Vector3(globalLightStartRotation.x, angle, 0), 0.25f);
    }
    
    public void StartWork(bool withAnim)
    {
        if (withAnim)
            Camera.main.transform.DOMove(CameraPos, 3f).SetEase(Ease.InOutQuad).OnComplete(() => isWorking = true);
        else
        {
            Camera.main.transform.position = CameraPos;
            DOTween.Sequence().AppendInterval(1f).OnComplete(() => isWorking = true);
        }
    }

    // private void Update()
    // {
    //     if (!isWorking)
    //         return;
    // }
}
