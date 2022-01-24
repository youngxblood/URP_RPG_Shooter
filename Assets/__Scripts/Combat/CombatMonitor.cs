using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMonitor : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] enemySpawners;
    [SerializeField] public CombatWave[] combatWaves;
    [SerializeField] private Collider2D[] collisionTrigger;
    private int currentWave = 0;
    private bool readyToStartWave = false;

    [SerializeField] private List<GameObject> currentEnemiesAlive = new List<GameObject>();

    public bool ReadyToStartWave
    {
        get { return readyToStartWave; }
        set { readyToStartWave = value; }
    }

    // Combat Monitor settings
    public bool useTriggerToStartCombat = false;
    public bool triggerIsActive = false;

    // Get spawners and waves in children
    private void Awake()
    {
        enemySpawners = GetComponentsInChildren<EnemySpawner>();
        combatWaves = GetComponentsInChildren<CombatWave>();
        collisionTrigger = GetComponentsInChildren<Collider2D>();
    }

    private void Update()
    {
        if (ReadyToStartWave)
        {
            if (useTriggerToStartCombat && triggerIsActive)
            {
                StartNextWave(currentWave);
                ReadyToStartWave = false;
            }
            if (!useTriggerToStartCombat)
            {
                StartNextWave(currentWave);
                ReadyToStartWave = false;
            }
        }
    }

    # region Helpers
    // Handles starting the wave, not spawning
    private void StartNextWave(int waveNumber)
    {
        SpawnEnemiesInWave(waveNumber);
    }

    private void SpawnEnemiesInWave(int wave)
    {
        // Gets how many entries are in each wave
        int entriesInWave = combatWaves[wave].enemies.Length;

        for (int i = 0; i < entriesInWave; i++)
        {
            for (int j = 0; j < combatWaves[wave].GetEnemyCountInWave(i); j++)
            {
                GameObject spawnedEnemy = Instantiate(combatWaves[wave].GetEnemyGameObject(i)) as GameObject;
                var enemy = spawnedEnemy.GetComponent<Enemy>();
                enemy.combatMonitor = this;
                currentEnemiesAlive.Add(spawnedEnemy); // Adds enemies to list
            }

        }
    }

    public void RemoveDeadEnemiesFromList()
    {
        for (int i = currentEnemiesAlive.Count - 1; i >= 0; i--)
        {
            if(currentEnemiesAlive[i] == null)
                currentEnemiesAlive.RemoveAt(i);
        }
    }

    // Icon for visualization of where combat monitor is
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "Monitor_Icon_White.png", true);
    }

    # endregion
}
