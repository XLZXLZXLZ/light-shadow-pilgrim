using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public PathNode A, B;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            MapRotateController.Instance.Rotate(90);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            MapRotateController.Instance.Rotate(-90);
        }
    }
}
