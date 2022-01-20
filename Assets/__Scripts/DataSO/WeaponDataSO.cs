using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    [field: SerializeField] public BulletDataSO BulletData {get; set;}
    [field: SerializeField] [field: Range(0, 100)] public int ammoCapacity { get; set; } = 100;
    [field: SerializeField] public bool AutomaticFire { get; set; } = false;
    [field: SerializeField] [field: Range(0.1f, 2f)] public float WeaponDelay { get; set; } = 0.1f;
    [field: SerializeField] [field: Range(0f, 10f)] public float SpreadAngle { get; set; } = 5f;
    [SerializeField] private bool multiBulletShot = false; // Use for shotguns
    [SerializeField] [Range(1, 10)] private int bulletCount = 1; // Increase for shotguns


    internal int GetBulletCountToSpawn()
    {
        if(multiBulletShot)
        {
            return bulletCount;
        }
        return 1;
    }
}
