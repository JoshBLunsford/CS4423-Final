using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShieldHPDisplay : MonoBehaviour
{
    public TextMeshProUGUI hp, shield;
    PlayerStats ps;
    private void Start()
    {
        ps = GeneralAssist.ps;
        ps.hpChangedBroadcast += UpdateUI;
        UpdateUI();
        
    }
    public void UpdateUI()
    {
        shield.text = $"SHIELD: {ps.currentShield}/{ps.maxShield}";
        hp.text = $"HP: {ps.currentHp}/{ps.maxHp}";
    }
}
