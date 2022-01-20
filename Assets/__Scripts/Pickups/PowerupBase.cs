using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBase : MonoBehaviour
{
    protected float powerupDuration;
    protected Player player;

    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();    
    }
}
