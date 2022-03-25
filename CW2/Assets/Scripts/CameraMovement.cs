using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    public Transform player;    // player transform
    public Vector3 CamOffset;   // public offest for camera


    // Camera follows players position with a offset.
    void Update()
    {
        transform.position = CamOffset + player.position;
    }
}
