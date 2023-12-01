using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour 
{
    //public float fov, lookTime;
    //public EnemyStats stats;
    //PlayerStats ps;
    public AudioSource shootsound;
    public AIPath aIPath;
    public Seeker seeker;
    public GenericShoot shoot;
    public GameObject raycastOrigin;
    public int damage;
    public bool aggro;
    float fireElapsed = 0;
    public float fireRate;
    float destinationElapsed = 0;
    const float destinationWait = 0.1f;
    //public Transform target;

    public BulletTracer[] tracers;
    int tIndex = 0;
    //public Vector2 velocity;

    private void Update()
    {
        
        if (PauseMenu.paused || !aggro)
        {
            aIPath.canMove = false;
            return;
        }
        aIPath.canMove = true;
        fireElapsed += Time.deltaTime;
        if (fireElapsed >= fireRate)
        {
            shootsound.Play();
            fireElapsed = 0;
            var hit = shoot.Shoot();
            tracers[tIndex].Draw(raycastOrigin.transform.position, hit.point);
            tIndex = tIndex + 1 >= tracers.Length ? 0 : tIndex + 1;
            if (hit.transform.CompareTag("Player"))
            {
                hit.transform.GetComponent<PlayerStats>().TakeDamage(damage);
            }
        }
        destinationElapsed += Time.deltaTime;
        if (destinationElapsed >= destinationWait)
        {
            aIPath.destination = GeneralAssist.ps.transform.position;
            destinationElapsed = 0;
        }

    }
}
   