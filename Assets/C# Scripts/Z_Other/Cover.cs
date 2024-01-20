using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cover : Singleton<Cover>
{
    Image i;

    protected override void Awake() 
    {
        base.Awake();
        GenerateCover();
    }

    private void GenerateCover()
    {
        var c = this.AddComponent<Canvas>();
        c.renderMode = RenderMode.ScreenSpaceOverlay;
        c.sortingOrder = 100;

        var go = new GameObject();
        go.transform.SetParent(this.transform, false);

        i = go.AddComponent<Image>();
        i.color = new Color(209/255f,195f/255,168f/255);
        i.transform.localScale = new Vector3(10000, 10000);
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeScene(string sceneName)
    {
        ChangeScene(sceneName, 1f);
    }
    public void ChangeScene(string sceneName,float time)
    {
         ChangeScene(sceneName,time/2,0);
    }
    public void ChangeScene(string sceneName, float time,float holdTime)
    {
        if (isChanging)
            return;
        StartCoroutine(ChangingScene(sceneName, time / 2, holdTime));
    }


    private bool isChanging;

    private IEnumerator ChangingScene(string sceneName, float time, float holdTime)
    {
        isChanging = true;
        i.color -= new Color(0, 0, 0, 1f);
        while (i.color.a < 1)
        {
            i.color += new Color(0, 0, 0, Time.deltaTime / time);
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(holdTime);
        while (i.color.a > 0)
        {
            i.color -= new Color(0, 0, 0, time * Time.deltaTime / time);
            yield return null;
        }
        isChanging = false;
    }
}
