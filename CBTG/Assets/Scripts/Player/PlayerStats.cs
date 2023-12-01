using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region struct defs
    public struct IEntry
    {
        public ItemData data;
        public int numberOfItems;
        public IEntry(ItemData d)
        {
            data = d;
            numberOfItems = 1;
        }
        public void Increment()
        {
            numberOfItems++;
        }
    }
    #endregion

    #region delegates
    public delegate void HitEnemy(RaycastHit2D hit, ref int damage);
    public HitEnemy hitEnemyBroadcast;

    public delegate void TookDamage(ref float damage);
    public TookDamage tookDamageBroadcast;

    public delegate void ShieldBroke();
    public ShieldBroke shieldBrokeBroadcast;

    public delegate void HPChanged();
    public HPChanged hpChangedBroadcast;
    #endregion

    public int standardDamage, currentHp, maxHp, currentShield, maxShield;
    public ShieldData shield;
    public List<IEntry> allItems = new List<IEntry>();
    public Stats baseStats, currentStats;

    float charge;



    private void Awake()
    {
        GeneralAssist.ps = this;
        currentStats = baseStats;
        currentHp = maxHp;
        maxShield = currentShield = shield.shieldVal;
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
    public int GetStackNum(ItemData item)
    {
        foreach (IEntry entry in allItems)
        {
            if (item == entry.data)
                return entry.numberOfItems;
        }
        return 0;
    }
    public void ChangeShield(ShieldData newShield)
    {
        shield = newShield;
        maxShield = shield.shieldVal + (int)currentStats.bonusShield;
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
        currentHp += (int)newStats.bonusHp;
        if (currentHp == 0)
            currentHp = 1;
        maxHp += (int)newStats.bonusHp;
        maxShield += (int)newStats.bonusShield;
        standardDamage += (int)newStats.bonusDamage;
        hpChangedBroadcast.Invoke();
    }
    public void TakeDamage(float dmg)
    {
        int chp = currentHp, csh = currentShield;
        if (tookDamageBroadcast != null)
            tookDamageBroadcast.Invoke(ref dmg);
        if (currentShield > 0)
        {
            currentShield -= (int)dmg;
            if (currentShield <= 0)
                currentShield = 0;
        }
        else
        {
            currentHp -= (int)dmg;
            if (currentHp <= 0)
            {
                currentHp = 0;
                //do death things
            }
        }

        if (chp != currentHp || csh != currentShield)
        {
            hpChangedBroadcast.Invoke();
            charge = 0;
        }
    }
    public void ShotEnemy(RaycastHit2D hit)
    {
        EnemyStats enemy = hit.transform.GetComponent<EnemyStats>();
        int dmg = standardDamage;
        if (hitEnemyBroadcast != null)
            hitEnemyBroadcast.Invoke(hit, ref dmg);
        enemy.TakeDamage(dmg);
    }
    void Update()
    {
        if (currentShield != maxShield)
        {
            charge += Time.deltaTime;
            if (charge * currentStats.shieldDelayMult >= shield.regenDelay)
            {
                currentShield++;
                hpChangedBroadcast.Invoke();
                charge = 0;
            }
        }
        else
            charge = 0;
        if (currentHp <= 0)
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }


}
