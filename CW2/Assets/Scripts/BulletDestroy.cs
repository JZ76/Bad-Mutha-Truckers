// H00128802
// Script for destroying any bullet oncollision
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    public float damage = 10;

    // Destroy bullet on collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Debug.Log(collision.gameObject);
        PlayerMovement player = collision.transform.GetComponent<PlayerMovement>();
        
        if (player != null)
        {
            Debug.Log(player.health);
            player.TakeDamage(damage);
        }

        
    }
}
