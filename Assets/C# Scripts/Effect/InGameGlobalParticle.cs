using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameGlobalParticle : MonoSingleton<InGameGlobalParticle>
{
    
    void Start()
    {
        var canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }
}
