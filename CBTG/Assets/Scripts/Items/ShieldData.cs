using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShieldData : ItemData
{
    public int shieldVal;
    public float regenDelay;
    public override void ApplyChanges()
    {

    }

    public override void PickedUp()
    {
        GeneralAssist.ps.ChangeShield(this);
    }
    public abstract void OnTakeDamage();
    public abstract void OnDoDamage();
    public abstract void OnBreak();
}
