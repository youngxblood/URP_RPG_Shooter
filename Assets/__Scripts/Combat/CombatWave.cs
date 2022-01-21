using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatWave : MonoBehaviour
{
    public int waveID;
    [SerializeField] private int enemyCountInWave;
    [SerializeField] public EnemyInWave[] enemies;

    private void Start()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            EnemyInWave enemy = new EnemyInWave();
            // enemy[i] = enemy;
            enemy = enemies[i];
        }
    }

    public int GetEnemyCountInWave(int enemyID)
    {
        EnemyInWave enemy = new EnemyInWave();
        enemy = enemies[enemyID];
        return enemy.Count;
    }

    public GameObject GetEnemyGameObject(int enemyID)
    {
        EnemyInWave enemy = new EnemyInWave();
        enemy = enemies[enemyID];
        return enemy.enemyType;
    }
}

[Serializable]
public class EnemyInWave
{
    [SerializeField] public int count = 1;
    [SerializeField] public GameObject enemyType;

    public int Count
    {
        get { return count; }
    }

}
