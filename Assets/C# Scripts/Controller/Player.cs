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
    private Vector3 startScale;
    public bool CanMove { get; private set; } = true; //判断此时是否可以移动
    public float Speed => speed;

    #region Runtime

    protected override void Awake()
    {
        base.Awake();
        particle = GetComponentInChildren<ParticleSystem>();
        startScale = transform.localScale;
        EventManager.Instance.MapUpdate.OnStart += OnMapUpdateStart;
        EventManager.Instance.MapUpdate.OnFinished += OnMapUpdateFinished;
        EventManager.Instance.Transmit.OnStart += OnTransmitStart;
        EventManager.Instance.Transmit.OnFinished += OnTransmitFinished;
    }

    // private void Update()
    // {
    //     Debug.Log(CanMove);
    // }

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

    #endregion

    #region Public

    public void EndAnim()
    {
        Instantiate(destroyParticle, transform.position, Quaternion.identity);
        transform.position += Vector3.up * 10000;
    }

    public Tween OnSmaller()
    {
        return transform.DOScale(Vector3.zero, Consts.PlayerScaleTransformDuration);
    }

    public Tween OnBigger()
    {
        return transform.DOScale(startScale, Consts.PlayerScaleTransformDuration);
    }

    #endregion

    #region Events

    private void OnMapUpdateStart()
    {
        CanMove = false;
    }

    private void OnMapUpdateFinished()
    {
        CanMove = true;
    }

    private void OnTransmitStart()
    {
        CanMove = false;
    }

    private void OnTransmitFinished()
    {
        CanMove = true;
    }

    #endregion

    
}
