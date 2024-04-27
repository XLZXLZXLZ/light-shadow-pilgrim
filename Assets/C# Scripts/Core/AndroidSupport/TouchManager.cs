using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class TouchManager : MonoSingleton<TouchManager>
{
    public Action onAnyTouch;

    void Update()
    {
        // 检测是否有触摸输入
        if (Input.touchCount > 0)
        {
            onAnyTouch?.Invoke();

            Touch touch = Input.GetTouch(0);

            // 检测是否为触摸开始事件
            if (touch.phase == TouchPhase.Began)
            {
                // 将触摸点转换为世界坐标
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                // 发射射线检测点击的目标对象
                RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero, Mathf.Infinity);

                // 如果点击到了目标对象，则触发相应的函数
                if (hit.collider != null)
                {
                    if(hit.collider.TryGetComponent<IInteractable>(out var i))
                        i.OnInteract();
                }
            }
        }
    }
}
*/

