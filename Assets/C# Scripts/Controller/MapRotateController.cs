using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapRotateController : Singleton<MapRotateController>
{
    public bool IsRotating { get; private set; }
    public bool Interrupted { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        EventManager.Instance.MapUpdate.OnStart += () => Interrupted = false;
        EventManager.Instance.MapUpdate.OnFinished += () => Interrupted = true;
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
}
