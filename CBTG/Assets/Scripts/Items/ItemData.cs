using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite visual;

    public abstract void PickedUp();
    public abstract void ApplyChanges();
}
