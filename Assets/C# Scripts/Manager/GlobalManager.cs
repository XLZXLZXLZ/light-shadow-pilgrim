using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorText;
    [SerializeField]
    private Vector2 cursorBias;

    private void Awake()
    {
        Cursor.SetCursor(cursorText, cursorBias, CursorMode.Auto);
    }
}

