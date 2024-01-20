using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : UIPanel
{
    private bool isOn = false;

    public void Continue()
    {
        Hide();
    }

    public void BackToMenu()
    {
        Cover.Instance.ChangeScene("MainMenu",1f,2f);
        Time.timeScale = 1f;
        Hide();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public override void Show()
    {
        base.Show();
        Time.timeScale = 0f;
        isOn = true;
        foreach (var g in graphics)
            g.gameObject.SetActive(true);
    }

    protected override void HideComplete()
    {
        base.HideComplete();
        Time.timeScale = 1f;
        isOn = false;
        foreach(var g in graphics)
            g.gameObject.SetActive(false);
    }

    #region 帝吧格尔
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (!isOn)
                Show();
            else
                Hide();
        }
    }
    #endregion
}
