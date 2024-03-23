using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SYExtension
{
    // public async static UniTask DoBounceMove(this Transform transform, Vector3 pos, float duration)
    // {
    //     Vector3 overPos = transform.position + (pos - transform.position) * 1.1f;
    //     float overTime = duration * 1f / 2f;
    //     float backTime = duration * 1f / 2f;
    //
    //     transform.DOMove(overPos, overTime);
    //     await UniTask.Delay(TimeSpan.FromSeconds(overTime));
    //     transform.DOMove(pos, backTime);
    //     await UniTask.Delay(TimeSpan.FromSeconds(backTime));
    // }

    public static void PushToGameObjectPool(this GameObject go)
    {
        PoolManager.Instance.PushGameObject(go);
    }
}

