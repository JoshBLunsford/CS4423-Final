using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera mainCam;
    public Rigidbody2D rb;
    PlayerStats stats;
    private void Start()
    {
        stats = PlayerStats.god;
    }

    private void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * stats.currentStats.speed, Input.GetAxis("Vertical") * stats.currentStats.speed);
        Vector2 objectPos = mainCam.WorldToScreenPoint(transform.position);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(Input.mousePosition.y - objectPos.y, Input.mousePosition.x - objectPos.x) * Mathf.Rad2Deg));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            collision.gameObject.SendMessage("PickUp");
        }
    }
}
