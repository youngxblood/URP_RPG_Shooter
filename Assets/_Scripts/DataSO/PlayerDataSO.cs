using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    public int health;
    public int maxHealth;
    public int maxLives;
    public float movementSpeed;
    public float acceleration = 50f;
    public float deacceleration = 50f;

    private void Awake() 
    {
        maxHealth = health;    
    }
}
