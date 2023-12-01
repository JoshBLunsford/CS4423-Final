using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Panic Button", menuName = "Tow/ScriptObjs/Custom/Panic Button")]
public class PanicButton : ItemData
{
    PlayerStats ps;
    int stackToWithdraw = 0;
    public override void ApplyChanges(){}
    public override void PickedUp()
    {
        PickedUpGeneric();
        ps = GeneralAssist.ps;
        ps.hpChangedBroadcast += CheckSpeed;
    }

    public void CheckSpeed()
    {
        Debug.Log($"Stack to withdraw: {stackToWithdraw}");
        if (ps.currentShield == 0)
        {
            stackToWithdraw += 2;
            ps.currentStats.speed += 2;
        }
        else
        {
            ps.currentStats.speed -= stackToWithdraw;
            stackToWithdraw = 0;
        }
    }
}
