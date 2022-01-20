using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture = null;

    private void Start() 
    {
        SetCursorIcon();    
    }

    private void SetCursorIcon()
    {
        //SetCursor takes the cursor image, the center point and the "CursorMode"
        UnityEngine.Cursor.SetCursor(cursorTexture, new Vector2(cursorTexture.width/2f, cursorTexture.height/2f), CursorMode.Auto); 
    }
}
