// H00128802
// Script for destroying any bullet oncollision
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{   
    // Destroy bullet on collision
    void OnCollisionEnter2D(Collision2D collision)
    {
             Destroy(gameObject);   
    }  
}
