using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    public enum ItemType { Stat, Passive, Shield }

    public string itemName;
    public string description;
    public ItemType itemType;
    public Rarity rarity;
    public Sprite visual;

    public ItemData()
    {
        if (this is StatItemData)
            itemType = ItemType.Stat;
        else if (this is ShieldData)
            itemType = ItemType.Shield;
        else
            itemType = ItemType.Passive;
    }

    public abstract void PickedUp();
    protected void PickedUpGeneric()
    {
        GeneralAssist.ps.AddToInventory(this);
    }
    public abstract void ApplyChanges();
}
