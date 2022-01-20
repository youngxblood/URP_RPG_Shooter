using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMonitor : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] enemySpawners;
    [SerializeField] private GameObject[] enemyTypes;

    private void Awake() 
    {
        if (enemySpawners != null)
        {
            enemySpawners[0].SpawnEnemy(enemyTypes[0]);
            enemySpawners[0].SpawnEnemy(enemyTypes[1]);
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "Monitor_Icon.png", true);
    }
}
