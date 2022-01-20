using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    [field: SerializeField] public int MaxHealth { get; set; } = 3;
    [field: SerializeField] public float AttackRange { get; set; } = 1f;
    [field: SerializeField] public int Damage { get; set; } = 1;
    [field: SerializeField] public GameObject DeathVFX { get; set; }
}
