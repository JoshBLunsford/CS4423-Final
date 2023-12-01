using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public static ItemPickup ip;
    public Tween uiRoutine;
    public UIBlink blink;
    public Image visual;
    public TextMeshProUGUI itemName, desc;
    public float closeTime = 5;

    float elapsed = 0;
    private void Start()
    {
        ip = this;
    }
    public void PickedUp(ItemData item)
    {
        visual.sprite = item.visual;
        itemName.text = item.itemName;
        desc.text = item.description;
        if (blink.open)
        {
            blink.Blink();
            elapsed = 0;
        }
        else
            uiRoutine.Enact();
    }

    private void Update()
    {
        if (blink.open)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= closeTime)
                uiRoutine.Enact(true, uiRoutine.ReverseRoutine(), true);
        }
        else
            elapsed = 0;
        
    }
}
