using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeneralAssist
{
    public static PlayerStats ps;
    const float scaleVal = 1.08f;

    public static float Scale(int val1, int lvl, float scale = scaleVal)
    {
        return val1 * Mathf.Pow(scale, lvl - 1);
    }
    public static int ScaleInt(int val1, int lvl, float scale = scaleVal)
    {
        return (int)Scale(val1, lvl, scale);
    }
}
