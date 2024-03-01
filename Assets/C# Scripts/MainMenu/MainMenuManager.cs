using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//面向结果编程的啥卵代码，可能全都要改(
public class MainMenuManager : Singleton<MainMenuManager>
{
    [SerializeField]
    private Vector3 CameraPos;
    [SerializeField]
    private Material highLightMat;
    
    private bool isWorking;

    [SerializeField] private List<LevelItem> levelItems;
    [SerializeField] private Transform globalLight;

    private LinkedList<LevelItem> levelItemLinkList;
    private LinkedListNode<LevelItem> currentSelectedItemNode;

    private LinkedListNode<LevelItem> NextNode => currentSelectedItemNode.Next ?? levelItemLinkList.First;
    private LinkedListNode<LevelItem> PreviewNode => currentSelectedItemNode.Previous ?? levelItemLinkList.Last;
    private LevelItem CurrentSelectedItem => currentSelectedItemNode.Value;
    private int CurrentLevelIndex => CurrentSelectedItem.LevelIndex;
    private float RotateAngle => 360f / levelItems.Count;
    
    private void Start()
    {
        levelItemLinkList = new(levelItems);
        currentSelectedItemNode = levelItemLinkList.First;
        ChangeChoice(currentSelectedItemNode);
        SetCompleteMat();
    }

    private void SelectNext()
    {
        ChangeChoice(NextNode);
        globalLight.DOBlendableLocalRotateBy(new Vector3(0, RotateAngle, 0), 0.25f);
    }

    private void SelectPreview()
    {
        ChangeChoice(PreviewNode);
        globalLight.DOBlendableLocalRotateBy(new Vector3(0, -RotateAngle, 0), 0.25f);
    }
    
    //它是一段一段的屎山，但我现在懒得改了
    private void ChangeChoice(LinkedListNode<LevelItem> levelItemNode)
    {
        currentSelectedItemNode.Value.MoveDown();
        currentSelectedItemNode = levelItemNode;
        currentSelectedItemNode.Value.MoveUp();
    }

    /// <summary>
    /// 为通关部分设置发光材质
    /// </summary>
    private void SetCompleteMat()
    {
        levelItems.ForEach(item =>
        {
            if (StaticData.IsLevelCompleted(item.LevelIndex))
            {
                item.SetMat(highLightMat);
            }
        });
    } 
    
    public void ChooseLevel()
    {
        Cover.Instance.ChangeScene("Level" + CurrentLevelIndex,2);
    }
    
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

    #region debug
    private void Update()
    {
        if (!isWorking)
            return;
        if (Input.GetKeyDown(KeyCode.Q))
            SelectPreview();

        if (Input.GetKeyDown(KeyCode.E))
            SelectNext();

        if (Input.GetKeyDown(KeyCode.Space))
            ChooseLevel();
    }
    #endregion
}
