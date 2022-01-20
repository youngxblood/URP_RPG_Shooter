using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionPad : MonoBehaviour
{
    private LevelManager levelManager;

    private void Awake() 
    {
        levelManager = FindObjectOfType<LevelManager>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))  
        {
            levelManager.LoadNextScene();
        }
    }
}
