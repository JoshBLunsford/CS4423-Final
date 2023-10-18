using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat Item", menuName = "Tow/ScriptObjs/StatItem")]
public class StatItemData : ItemData
{
    public Stats statChange;

    public override void PickedUp()
    {
        PlayerStats.god.AddToInventory(this);
    }
    public override void ApplyChanges()
    {
        PlayerStats.god.ChangeStats(statChange, Stats.ChangeType.Add);
    }
    
}
