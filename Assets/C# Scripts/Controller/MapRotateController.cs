using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapRotateController : MonoBehaviour
{
    public bool IsRotating { get; private set; }
    public bool Interrupted { get; private set; }

    private int rotateIndex = 0;

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

        if (angle >= 0)
        {
            //AudioManager.Instance.PlaySe(AudioName.RotateMapShun + rotateIndex);
            rotateIndex = (rotateIndex + 5) % 4;
        }
        else
        {
            //AudioManager.Instance.PlaySe(AudioName.RotateMapNi + rotateIndex);
            rotateIndex = (rotateIndex + 3) % 4;
        }

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

#if UNITY_ANDROID
        SlideDetect();
#endif
    }

    private void EnableRotate()
    {
        Interrupted = false;
    }

    private void DisableRotate()
    {
        Interrupted = true;
    }

#if UNITY_ANDROID
    // 用于记录触摸起始位置
    private Vector2 touchStartPos;

    // 定义滑动的最小距离阈值
    public float minSwipeDistance = 50f;

    void SlideDetect()
    {
        // 检测是否有触摸输入
        if (Input.touchCount > 0)
        {
            // 获取第一个触摸点的信息
            Touch touch = Input.GetTouch(0);

            // 判断触摸的阶段
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // 记录触摸起始位置
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    // 计算滑动距离
                    float swipeDistance = (touch.position - touchStartPos).magnitude;

                    // 如果滑动距离超过阈值，则认为是有效的滑动
                    if (swipeDistance > minSwipeDistance)
                    {
                        // 计算滑动方向
                        Vector2 swipeDirection = touch.position - touchStartPos;

                        // 水平方向的滑动
                        if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                        {
                            // 判断滑动方向并调用相应的函数
                            if (swipeDirection.x > 0)
                            {
                                // 向右滑动，调用 Func(1)
                                Rotate(90);
                            }
                            else
                            {
                                // 向左滑动，调用 Func(-1)
                                Rotate(-90);
                            }
                        }
                    }
                    break;
            }
        }
    }
#endif
}
