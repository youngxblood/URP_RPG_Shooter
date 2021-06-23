using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementData : MonoBehaviour
{
    [field: SerializeField] public Vector2 Direction { get; set; }
    [field: SerializeField] public Vector2 PointOfInterest { get; set; } // Is named PointOfInterest as some enemies might act differently and not just seek the player
}
