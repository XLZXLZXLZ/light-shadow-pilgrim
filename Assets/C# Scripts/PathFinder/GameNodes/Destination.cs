using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destination : MonoBehaviour, ITriggerable
{
    public void OnTrigger()
    {
        EventManager.Instance.OnGameOver.Invoke();
        StaticData.CompleteLevel(GameManager.Instance.currentLevel);
        
        var player = Player.Instance;
        var cam = Camera.main;

        // 保留原来的角色通关动画
        player.EndAnim();
        DOTween.Sequence()
            .Append(cam.transform.DOMoveY(Consts.GameOverCamUpHeight, Consts.GameOverCamUpDuration))
            .Join(cam.DOOrthoSize(cam.orthographicSize / 2.5f, Consts.GameOverCamUpDuration)
                .SetEase(Ease.InOutQuad))
            .OnComplete(GameManager.Instance.ShowGameOverTip);
    }

    private void Complete() //通关动画(因为已经懒得做了所以极度面向结果编程的危险程序) *惊恐*
    {
        // Player.Instance.InterruptMovement(100);//终止玩家操作
        // DOTween.Sequence()
        //     .Append(cam.transform.DOMove(player.transform.position + cam.transform.forward * -10, 0.5f).SetEase(Ease.InOutQuad))
        //     .Join()
        //     .Append(cam.DOShakePosition(1f,0).OnComplete ())
        //     .AppendInterval(2f).OnComplete(() => Cover.Instance.ChangeScene("MainMenu", 2, 1));
    }

    public void OnTriggerOver()
    {
        
    }
}
