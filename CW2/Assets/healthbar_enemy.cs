using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthbar_enemy : MonoBehaviour
{
    public Transform enemy;
    public Vector3 Offset = new Vector3(0f,2f,0f);

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f,0f,0f);
        transform.position = Offset + enemy.position;
    }

}
