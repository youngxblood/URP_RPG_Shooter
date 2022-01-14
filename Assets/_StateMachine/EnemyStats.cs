using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] protected EnemyDataSO enemyData;
    [SerializeField] protected MovementDataSO movementData;
    public float attackRange = 1f;

    public EnemyDataSO EnemyData
    {
        get { return enemyData; }
        set { enemyData = value; }
    }
    
    public MovementDataSO MovementData
    {
        get { return movementData; }
        set { movementData = value; }
    }
    
}
