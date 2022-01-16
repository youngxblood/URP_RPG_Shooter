using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAIBrain : MonoBehaviour
{
    [field: SerializeField] public GameObject Target { get; set; }

    private void Awake() 
    {
        Target = FindObjectOfType<Player>().gameObject; // NOTE: Replace this in the future?
    }
}