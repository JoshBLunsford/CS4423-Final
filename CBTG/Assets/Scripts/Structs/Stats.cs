using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stats
{
    public enum ChangeType { Add, Subtract, Overwrite /*, IntoAFly*/ }
    public float speed, fireRate, damageMult, accuracy;
    public Stats(float s, float fr, float dm, float a)
    {
        speed = s;
        fireRate = fr;
        damageMult = dm;
        accuracy = a;
    }
    static Stats DoOperation(Stats x, Stats y, float n)
    {
        return new Stats(x.speed + n * y.speed, x.fireRate + n * y.fireRate, x.damageMult + n * y.damageMult, x.accuracy + n * y.accuracy);
    }
    //+ and - operators reference the same operation, but - makes y negative.
    public static Stats operator +(Stats x, Stats y)
    {
        return DoOperation(x, y, 1f);
    }
    public static Stats operator -(Stats x, Stats y)
    {
        return DoOperation(x, y, -1f);
    }
}
