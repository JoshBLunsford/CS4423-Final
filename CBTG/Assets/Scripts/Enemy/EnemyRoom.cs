using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoom : MonoBehaviour
{
    public EnemyMovement[] enemies;

    private void Start()
    {
        StartCoroutine(CheckForAggro());
    }
    IEnumerator CheckForAggro()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            bool setAggro = false;
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].aggro && !setAggro)
                {
                    setAggro = true;
                    i = 0;
                }
                enemies[i].aggro = setAggro;
            }
            if (setAggro)
                yield break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (EnemyMovement enemy in enemies)
                enemy.aggro = true;
        }
    }
}
