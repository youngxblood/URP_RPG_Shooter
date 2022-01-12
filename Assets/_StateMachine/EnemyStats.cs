using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] protected MovementDataSO movementData;
    public float attackRange = 1f;

    public MovementDataSO MovementData
    {
        get { return movementData; }
        set { movementData = value; }
    }
    
}
