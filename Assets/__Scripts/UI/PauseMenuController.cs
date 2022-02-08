using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public Canvas pauseMenuCanvas;
    public Canvas optionsMenuCanvas;

    private void Awake() 
    {
        pauseMenuCanvas = GetComponent<Canvas>();    
    }

    public void OpenOptionsMenu()
    {
        optionsMenuCanvas.enabled = true;
        pauseMenuCanvas.enabled = false;
    }
    
    public void CloseOptionsMenu()
    {
        optionsMenuCanvas.enabled = false;
        pauseMenuCanvas.enabled = true;
    }
}
