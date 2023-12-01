using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int currentHp, maxHp;
    public static int totalEnemies;
    EnemyMovement movement;

    private void Awake()
    {
        totalEnemies++;
        movement = GetComponent<EnemyMovement>();
        currentHp = maxHp;
    }
    public void TakeDamage(int dmg)
    {

        movement.aggro = true;
        currentHp -= dmg;
        if (currentHp <= 0)
        {
            totalEnemies--;
            Destroy(gameObject);
        }
    }

}
