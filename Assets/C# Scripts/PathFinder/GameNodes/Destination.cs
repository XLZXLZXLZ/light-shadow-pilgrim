using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destination : MonoBehaviour, ITriggerable
{
    public void OnTrigger()
    {
        Complete();
    }

    private void Complete() //通关动画(因为已经懒得做了所以极度面向结果编程的危险程序) *惊恐*
    {
        Player.Instance.InterruptMovement(100);//终止玩家操作
        MapRotateController.Instance.interrupted = true; //禁止旋转地图

        var player = Player.Instance;
        var cam = Camera.main;
        DOTween.Sequence()
            .Append(cam.transform.DOMove(player.transform.position + cam.transform.forward * -10, 0.5f).SetEase(Ease.InOutQuad))
            .Join(cam.DOOrthoSize(cam.orthographicSize / 2.5f, 0.5f).SetEase(Ease.InOutQuad))
            .Append(cam.DOShakePosition(1f,0).OnComplete (player.EndAnim))
            .AppendInterval(2f).OnComplete(() => Cover.Instance.ChangeScene("MainMenu", 2, 1));
        StaticData.CompleteLevel(GameManager.Instance.currentLevel);
    }

    public void OnTriggerOver()
    {
        
    }
}
