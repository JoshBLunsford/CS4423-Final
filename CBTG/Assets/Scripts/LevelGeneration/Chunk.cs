using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Vector2 size, gridPos;
    public Vector2[] activeSpace, inPorts, outPorts;

    public int RandomOutPort
    {
        get
        {
            return Random.Range(0, outPorts.Length);
        }
    }
    public int RandomInPort
    {
        get
        {
            return Random.Range(0, inPorts.Length);
        }
    }
}
