using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DefaultShoot : MonoBehaviour
{

    public Transform firePoint;
    public GameObject pewPrefab;

    public float pewForce = 100f;

    void Update()
    {
        
        if(Input.GetKeyDown(InputAssist.shoot))
        {
            Shoot();
        }
        
    }

    void Shoot()
    {
        
        GameObject pew = Instantiate(pewPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = pew.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * pewForce, ForceMode2D.Impulse);

    }
}
