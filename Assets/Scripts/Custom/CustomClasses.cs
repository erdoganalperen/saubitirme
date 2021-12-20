using System;
using UnityEngine;

[Serializable]
public class RoomMapPosition
{
    public RoomMapPosition(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public float x;
    public float y;
    public float z;
}