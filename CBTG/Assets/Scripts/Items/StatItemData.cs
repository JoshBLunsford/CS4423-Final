using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat Item", menuName = "Tow/ScriptObjs/Stat Item")]
public class StatItemData : ItemData
{
    public float stackEfficacy;
    public Stats statChange;
    
    public override void PickedUp()
    {
        PickedUpGeneric();
    }
    public override void ApplyChanges()
    {
        GeneralAssist.ps.ChangeStats(statChange, Stats.ChangeType.Add);
    }
    
}
