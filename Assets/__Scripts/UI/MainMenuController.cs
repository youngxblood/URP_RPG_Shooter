using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private Canvas optionsMenuCanvas;
    [SerializeField] private LevelManager levelManager;

    private void Awake() 
    {
        levelManager = FindObjectOfType<LevelManager>();    
    }

    public void StartNewGame()
    {
        levelManager.LoadNextScene(); // Loads the next scene, which should be the first level
    }

    public void OpenOptionsMenu()
    {
        optionsMenuCanvas.enabled = true;
        mainMenuCanvas.enabled = false;
    }
    
    public void CloseOptionsMenu()
    {
        optionsMenuCanvas.enabled = false;
        mainMenuCanvas.enabled = true;
    }

    public void ResumeCurrentGame()
    {
        // To be implemented
    }

    public void LoadSelectedGame()
    {
        // To be implemented
    }

    public void CloseTitle()
    {
        Application.Quit();
    }


    //     private void HandlePauseMenu()
    // {
    //     if(Input.GetKeyDown(KeyCode.Escape))
    //     {
    //         if (!gameIsPaused)
    //             OpenPauseMenu();
    //         else
    //             ClosePauseMenu();
    //     }
    // }

    // public void OpenPauseMenu()
    // {
    //     Time.timeScale = 0f;
    //     pauseMenuCanvas.enabled = true;
    //     gameplayCanvas.enabled = false;
    //     gameIsPaused = true;
    // }

    // private void ClosePauseMenu()
    // {
    //     Time.timeScale = 1f;
    //     pauseMenuCanvas.enabled = false;
    //     gameplayCanvas.enabled = true;
    //     gameIsPaused = false;
    // }
}
