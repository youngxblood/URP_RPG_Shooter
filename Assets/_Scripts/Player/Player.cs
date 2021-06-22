using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IAgent
{
    public int Health {get; set;}
    public UnityEvent OnDeath { get; set; }
    public UnityEvent OnGetHit { get; set; }

}