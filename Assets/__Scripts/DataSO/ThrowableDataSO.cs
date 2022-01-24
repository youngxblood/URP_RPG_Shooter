using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Throwable")]
public class ThrowableDataSO : ScriptableObject
{
    [field: SerializeField] public GameObject grenadePrefab { get; set; }    
    [field: SerializeField] public GameObject explosionVFX { get; set; }
    [field: SerializeField] [field: Range(0, 50)] public int ammoCapacity { get; set; } = 24;
    [field: SerializeField] [field: Range(0, 50)] public int explosionDamage { get; set; } = 2;
    [field: SerializeField] [field: Range(0, 10)] public float explosionRadius { get; set; } = 2f;
    [field: SerializeField] public bool explodesOnImpact {get; set;} = false;
    [field: SerializeField] [field: Range(0, 7)] public float fuseTimer {get; set;} = 3f;
}
