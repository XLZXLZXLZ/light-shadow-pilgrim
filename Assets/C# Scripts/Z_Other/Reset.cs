using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
