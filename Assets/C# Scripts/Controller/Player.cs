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
    public bool CanMove { get; private set; } = true; //判断此时是否可以移动
    public float Speed => speed;    

    protected override void Awake()
    {
        base.Awake();
        particle = GetComponentInChildren<ParticleSystem>();
        EventManager.Instance.OnMapUpdateStart += () => SetPlayerCanMove(false);
        EventManager.Instance.OnMapUpdateFinished += () => SetPlayerCanMove(true);
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

    public void EndAnim()
    {
        Instantiate(destroyParticle, transform.position, Quaternion.identity);
        transform.position += Vector3.up * 10000;
    }

    public void SetPlayerCanMove(bool isCanMove)
    {
        CanMove = isCanMove;
    }
}
