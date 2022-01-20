using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnLocation;

    public void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy);
    }
}
