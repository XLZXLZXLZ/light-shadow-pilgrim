using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapRotateController : MonoBehaviour
{
    public bool IsRotating { get; private set; }
    public bool Interrupted { get; private set; }

    private void Awake()
    {
        EventManager.Instance.OnGameStart += EnableRotate;
        EventManager.Instance.OnGameOver += DisableRotate;
        EventManager.Instance.MapUpdate.OnStart += DisableRotate;
        EventManager.Instance.MapUpdate.OnFinished += EnableRotate;
    }

    public void Rotate(float angle)
    {
        if (IsRotating || Interrupted)
            return;

        IsRotating = true;
        transform.DORotate(transform.eulerAngles + Vector3.up * angle, 0.33f)
            .SetEase(Ease.OutQuart)
            .OnComplete(() => IsRotating = false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Rotate(90);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Rotate(-90);
        }
    }

    private void EnableRotate()
    {
        Interrupted = true;
    }

    private void DisableRotate()
    {
        Interrupted = false;
    }
}
