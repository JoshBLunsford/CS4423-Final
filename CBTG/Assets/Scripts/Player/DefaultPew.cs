using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPew : MonoBehaviour
{
    public float damage = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        TempTarget target =collision.transform.GetComponent<TempTarget>();
        if(target != null)
            target.TakeDamage(damage);
        Destroy(gameObject);
    }
}
