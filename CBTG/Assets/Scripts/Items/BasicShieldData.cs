using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield Data", menuName = "Tow/ScriptObjs/BasicShield")]
public class BasicShieldData : ShieldData
{
    //This shield does absolutely nothing special
    public override void OnBreak()
    {
    }

    public override void OnDoDamage()
    {
    }

    public override void OnTakeDamage()
    {
    }
}
