using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera mainCam;
    public Transform player;
    public Vector2 anchor;
    public float smoothTime;
    public float normalToCamera = 0.2f;
    Vector2 velocity = Vector2.zero;
    void Update()
    {
        if (PauseMenu.paused)
            return;
        anchor = Vector2.Lerp(player.position, mainCam.ScreenToWorldPoint(Input.mousePosition), normalToCamera);
        Vector2 smooth = Vector2.SmoothDamp(transform.position, anchor, ref velocity, smoothTime);
        transform.position = new Vector3(smooth.x, smooth.y, -5);

    }
}
