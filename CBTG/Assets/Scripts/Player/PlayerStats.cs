using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats god;
    public List<IEntry> allItems = new List<IEntry>();
    public Stats baseStats, currentStats;

    private void Awake()
    {
        god = this;
        currentStats = baseStats;
    }

    public void AddToInventory(ItemData item)
    {
        foreach (IEntry entry in allItems)
        {
            if (entry.data == item)
            {
                entry.Increment();
                entry.data.ApplyChanges();
                return;
            }
        }
        allItems.Add(new IEntry(item));
        allItems[allItems.Count - 1].data.ApplyChanges();
    }
    public void ChangeStats(Stats newStats, Stats.ChangeType change)
    {
        switch (change)
        {
            case Stats.ChangeType.Add:
                currentStats += newStats;
                break;
            case Stats.ChangeType.Subtract:
                currentStats -= newStats;
                break;
            case Stats.ChangeType.Overwrite:
                currentStats = newStats;
                break;

        }
    }

    public struct IEntry
    {
        public ItemData data;
        public int numberOfItems;
        public IEntry(ItemData d)
        {
            data = d;
            numberOfItems = 1;
        }
        //this function is a dumb workaround for indexes not being returned by ref from lists
        public void Increment()
        {
            numberOfItems++;
        }
    }

}
