using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SerializableVector3
{
    public float x;
    public float y;
    public float z;

    public Vector3 GetPosition()
    {
        return new Vector3(x, y, z);
    }
}

[System.Serializable]
public class Stats
{
    public SerializableVector3 playerPos;
    public int health;
    public int lives;
}
