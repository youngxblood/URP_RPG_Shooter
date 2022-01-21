using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMonitor : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] enemySpawners;
    [SerializeField] public CombatWave[] combatWaves;
    private int currentWave = 0;

    // Get spawners and waves in children
    private void Awake()
    {
        enemySpawners = GetComponentsInChildren<EnemySpawner>();
        combatWaves = GetComponentsInChildren<CombatWave>();
    }

    // 
    private void Start()
    {
        if (enemySpawners != null && combatWaves.Length >= currentWave)
        {
            StartNextWave(0);
            currentWave++;
            // StartNextWave(1);
        }
    }

    // Handles starting the wave, not spawning
    private void StartNextWave(int waveNumber)
    {
        SpawnEnemies(waveNumber);

    }

    private void SpawnEnemies(int wave)
    {
        int waveSections = combatWaves[wave].enemies.Length;

        for (int i = 0; i < waveSections; i++)
        {
            for (int j = 0; j < combatWaves[wave].GetEnemyCountInWave(i); j++)
            {
                Instantiate(combatWaves[wave].GetEnemyGameObject(i));
            }

        }
    }




    // Visualization of where combat monitor is
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "Monitor_Icon.png", true);
    }
}
