using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
This script works very... unconventionally. All this script does is it assumes its GameObject is the firing point, it performs a raycast, and returns the 
info from that raycast to whatever called it. The calling script should handle all of the information to do with actually applying damage, on-hit effects, etc...
*/
public class GenericShoot : MonoBehaviour
{
    public RaycastHit2D Shoot()
    {
        return Physics2D.Raycast(transform.position, transform.right);
    }
}