using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRotateController : Singleton<MapRotateController>
{
    private bool isRotating;
    public bool interrupted;

    public void Rotate(float angle)
    {
        if (isRotating || interrupted)
            return;

        isRotating = true;
        transform.DORotate(transform.eulerAngles + Vector3.up * angle, 0.33f)
            .SetEase(Ease.OutQuart)
            .OnComplete(() => isRotating = false);
    }
}
