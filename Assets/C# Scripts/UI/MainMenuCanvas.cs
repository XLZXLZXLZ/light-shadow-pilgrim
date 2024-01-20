using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MainMenuCanvas : MonoBehaviour
{
    private static bool firstEnter = true;
    private Animator anim;
    private bool isReady;

    private void Awake()
    {
        anim = GetComponent<Animator>();  
        if(!firstEnter)
        {
            EnterChooseLevel();
            Destroy(gameObject);
        }    
    }

    public void AnimationTrigger()
    {
        isReady = true;
    }

    private void Update()
    {
        if(isReady && Input.anyKey)
        {
            EnterChooseLevel();
        }
    }

    private void EnterChooseLevel()
    {
        isReady = false;
        MainMenuManager.Instance.StartWork(firstEnter);
        anim.Play("DisappearAnim");
        firstEnter = false;
    }
}
