using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnLocation;
    List<Enemy> aliveEnemies = new List<Enemy>();

    public void SpawnEnemy(GameObject enemy)
    {
        GameObject spawnedEnemy = Instantiate(enemy) as GameObject;
        Enemy newEnemy = spawnedEnemy.GetComponent<Enemy>(); // Get Enemy component from spawned GameObject
        aliveEnemies.Add(newEnemy);
    }
}
