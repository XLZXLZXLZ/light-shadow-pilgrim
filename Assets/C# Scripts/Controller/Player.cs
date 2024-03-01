using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private GameObject destroyParticle;

    private float shieldMovementTime; //记录屏蔽移动信息的时间
    private ParticleSystem particle;
    public bool SetCanMove { get; private set; } = true;
    public LightState PlayerLightState { get; private set; } = LightState.Light;
    public bool CanMove => SetCanMove && PlayerLightState == LightState.Light; //判断此时是否可以移动
    public float Speed => speed;    

    // public void InterruptMovement(float shieldTime) //外界输入冲断移动信号
    // {
    //     var t = Time.time + shieldTime;
    //     if (t > shieldMovementTime)
    //         shieldMovementTime = t;
    // }

    protected override void Awake()
    {
        base.Awake();
        particle = GetComponentInChildren<ParticleSystem>();
        EventManager.Instance.OnMapUpdateStart += () => SetPlayerCanMove(false);
        EventManager.Instance.OnMapUpdateFinished += () => SetPlayerCanMove(true);
    }

    private void Update()
    {
        // Debug.Log(CanMove);
    }

    private void Start() //出现动画
    {
        float recordSpeed = speed;
        particle.Stop();
        Vector3 scale = transform.localScale;
        speed = 0;
        transform.localScale = Vector3.zero;

        EventManager.Instance.OnGenerateMapFinished += () =>
        {
            DOTween.Sequence()
                .Append(transform.DOScale(scale, 1)
                    .OnComplete(() => 
                    {
                        speed = recordSpeed;
                        particle.Play();
                    }));
        };
    }

    public void EndAnim()
    {
        Instantiate(destroyParticle, transform.position, Quaternion.identity);
        transform.position += Vector3.up * 10000;
    }

    public void SetPlayerCanMove(bool isCanMove)
    {
        SetCanMove = isCanMove;
    }
}
