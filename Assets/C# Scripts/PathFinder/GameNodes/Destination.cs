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
            .AppendInterval(1f)
            .Append(cam.transform.DOMoveY(Consts.GameOverCamUpHeight, Consts.GameOverCamUpDuration)
                .SetEase(Ease.InQuad))
        // 芝士直接返回菜单的新增逻辑
            .AppendCallback(() => Cover.Instance.ChangeScene(Consts.MainMenuSceneName, Camera.main.backgroundColor, 3f, 2f));

        // .Join(cam.DOOrthoSize(cam.orthographicSize / 2.5f, Consts.GameOverCamUpDuration))
        // .OnComplete(GameManager.Instance.ShowGameOverTip);
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
