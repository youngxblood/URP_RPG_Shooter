using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private Scene currentScene;
    private Scene nextScene;
    private Scene[] allScenes;
    public List<string> scenes;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();

        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneCount; i++)
        {
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
            scenes.Add(sceneName);
        }
    }

    public void LoadNextScene()
    {
        int nextSceneID = SceneManager.GetActiveScene().buildIndex;
        if (scenes.Count > nextSceneID + 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
