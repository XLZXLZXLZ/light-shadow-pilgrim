using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//面向结果编程的啥卵代码，可能全都要改(
public class MainMenuManager : Singleton<MainMenuManager>
{
    [SerializeField]
    private int maxCount = 8;
    [SerializeField]
    private Transform clock;
    [SerializeField]
    private Vector3 CameraPos;
    [SerializeField]
    private Material highLightMat;

    private Transform globalLight;
    private int index;
    private bool isWorking;

    protected override void Awake()
    {
        base.Awake();
        globalLight = FindObjectOfType<Light>().transform;
    }

    private void Start()
    {
        ChangeChoice(0);
        SetCompletMat();
    }

    private void SetCompletMat()
    {
        for (int i = 0; i < clock.GetChild(1).childCount; i++)
        {
            if (StaticData.LevelCompleted[i])
            {
                var renderer = clock.GetChild(1).GetChild(i).GetComponentsInChildren<Renderer>();
                foreach (var m in renderer)
                {
                    m.material = highLightMat;
                }
            }
        }
    } //为通关部分设置发光材质

    public void StartWork(bool withAnim)
    {
        if (withAnim)
            Camera.main.transform.DOMove(CameraPos, 3f).SetEase(Ease.InOutQuad).OnComplete(() => isWorking = true);
        else
        {
            Camera.main.transform.position = CameraPos;
            DOTween.Sequence().AppendInterval(1f).OnComplete(() => isWorking = true);
        }
    }

    //它是一段一段的屎山，但我现在懒得改了
    public void ChangeChoice(int dir)
    {
        clock.GetChild(1).GetChild(index).DOMove( Vector3.zero, 0.25f);

        globalLight.DOBlendableLocalRotateBy(new Vector3(0, 45 * dir, 0), 0.25f);
        index = (index + dir + maxCount) % maxCount;

        clock.GetChild(1).GetChild(index).DOMove( Vector3.up * 0.5f, 0.25f);
    }

    public void Choose()
    {
        Cover.Instance.ChangeScene("Level" +  (index + 1),2);
    }

    #region debug
    private void Update()
    {
        if (!isWorking)
            return;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeChoice(-1);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeChoice(1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Choose();
        }
    }
    #endregion
}
