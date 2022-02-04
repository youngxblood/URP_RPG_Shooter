using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public PlayerDataSO playerData;
    public Stats stats;

    private void Awake() 
    {
        SetPlayerStats();    
    }

    public void SetPlayerStats()
    {
        stats.health = 10;
        stats.lives = 3;
    }
}
