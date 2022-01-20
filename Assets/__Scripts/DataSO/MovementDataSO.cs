using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Agent/MovementData")]
public class MovementDataSO : ScriptableObject
{
    [Range(1, 20)]
    public float maxSpeed = 5f;
    [Range(0.1f, 300)]
    public float acceleration = 50f, deacceleration = 50f;
}
