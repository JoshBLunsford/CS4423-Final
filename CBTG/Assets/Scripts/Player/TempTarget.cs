using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempTarget : MonoBehaviour
{
    
    public float hp = 10f;
    public float defaultHp;



    private void Start()
    {
        defaultHp = hp;
    }
    

    public void TakeDamage(float amount)
    {
        hp -= amount;
        if(hp <= 0 )
        {
            Die();
        }

        void Die()
        {

            hp = defaultHp;

            if (hp != defaultHp)
            {
                Destroy(gameObject);
            }
        }

    }
}
