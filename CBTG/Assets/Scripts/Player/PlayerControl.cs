using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public AudioSource shootsound;
    public Camera mainCam;
    public Rigidbody2D rb;
    public GenericShoot raycastOrigin;
    public BulletTracer[] tracers;
    public float fireRateBase = 0.2f;
    PlayerStats stats;
    int tIndex = 0;
    float shootElapsed = 0;
    private void Start()
    {
        stats = GeneralAssist.ps;
    }

    private void Update()
    {
        if (PauseMenu.paused)
        {
            rb.angularVelocity = 0;
            rb.velocity = Vector2.zero;
            return;
        }
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * stats.currentStats.speed, Input.GetAxis("Vertical") * stats.currentStats.speed);
        Vector2 objectPos = mainCam.WorldToScreenPoint(transform.position);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.Atan2(Input.mousePosition.y - objectPos.y, Input.mousePosition.x - objectPos.x) * Mathf.Rad2Deg) - 90));
        shootElapsed += Time.deltaTime;
        if (Input.GetKey(InputAssist.shoot) && shootElapsed * stats.currentStats.fireRate >= fireRateBase)
        {
            shootsound.Play();
            shootElapsed = 0;
            var hit = raycastOrigin.Shoot();
            tracers[tIndex].Draw(raycastOrigin.transform.position, hit.point);
            tIndex = tIndex + 1 >= tracers.Length ? 0 : tIndex + 1;
            if (hit.transform.CompareTag("Enemy"))
                stats.ShotEnemy(hit);
            print($"Hit: {raycastOrigin.Shoot().transform.name}");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            collision.gameObject.SendMessage("PickUp");
            ItemPickup.ip.PickedUp(collision.gameObject.GetComponent<Item>().data);
        }
    }
}
