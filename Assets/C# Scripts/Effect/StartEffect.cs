using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//开局时的动画效果，简单来说，是托屎山
public class StartEffect : MonoBehaviour
{
    [SerializeField]
    private bool useEffect = true;
    [SerializeField]
    private GameObject clickEffect;
    [SerializeField]
    private Transform backGround;

    private void Awake()
    {
        EventManager.Instance.OnClickNode += UseClickEffect;
        if (backGround == null)
            backGround = transform.Find("BackGround");
    }

    private void Start()
    {
        Effect();
    }

    private int counter;
    private int Counter
    {
        get => counter;
        set 
        { 
            counter = value; 
            if(counter == 0) 
                EventManager.Instance.OnGenerateMapFinished.Invoke();
        }
    }

    protected virtual void Effect() //游戏开始时的效果，为所有节点播放自下而上的动画(由机关移动的方块除外)
    {
        EventManager.Instance.OnGenerateMapStart.Invoke();
        
        for(int i = 0; i < transform.childCount; i++) 
        { 
            var child = transform.GetChild(i);
            if (child != backGround)
            {
                for (int j = 0; j < child.childCount; j++)
                {
                    var t = child.GetChild(j);
                    if (t.TryGetComponent<AppearBlock>(out var _)) //咱就无视掉要用机关触发的结点好了
                        continue;

                    Counter++;

                    Vector3 origin = t.position;
                    t.position += Vector3.down * 45;
                    float delay = (t.position.z - t.position.x + 10) * 0.2f;
                    delay = Mathf.Max(0, delay);

                    DOTween.Sequence()
                        .AppendInterval(delay)
                        .Append(t.DOMove(origin, 2).SetEase(Ease.OutQuart))
                        .OnComplete(() => Counter--);
                }
            }
            else
            {
                for (int j = 0; j < child.childCount; j++) //对背景层应用另外的生成方案
                {
                    var t = child.GetChild(j);
                    Vector3 origin = t.position;
                    t.position += Vector3.down * 45;

                    EventManager.Instance.OnGenerateMapFinished += () => {
                        DOTween.Sequence()
                            .Append(t.DOMove(origin, 2).SetEase(Ease.OutQuart))
                            .OnComplete(() => Counter--);
                    };
                }
            }
        }

    }

    private void UseClickEffect(PathNode node)
    {
        var go = Instantiate(clickEffect,node.transform.position,clickEffect.transform.rotation);
    }
}
