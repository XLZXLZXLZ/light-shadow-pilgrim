using MyExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class ControllerableMoveBlockButton : MonoBehaviour, IInteractable
{
    [SerializeField] Dir dir;

    ControllerableMoveBlock parent;

    public void OnInteract()
    {
        parent.Move(dir);
    }

    private void Awake()
    {
        parent = transform.parent.GetComponent<ControllerableMoveBlock>();
    }

    private void OnMouseDown()
    {
        OnInteract();
    }
}
