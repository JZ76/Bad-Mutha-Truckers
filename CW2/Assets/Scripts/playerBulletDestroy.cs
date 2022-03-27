// H00128802
// Script for destroying any bullet oncollision
using UnityEngine;

public class playerBulletDestroy : MonoBehaviour
{
    public float damage = 10;
    // Destroy bullet on collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);  
        EnemyController enemy_move = collision.transform.GetComponent<EnemyController>();
        EnemyControllerFixed enemy_fixed = collision.transform.GetComponent<EnemyControllerFixed>();
        if (enemy_move != null)
        {
            Debug.Log(enemy_move.health);
            enemy_move.TakeDamage(damage);
        }

        if (enemy_fixed != null)
        {
            Debug.Log(enemy_fixed.health);
            enemy_fixed.TakeDamage(damage);
        }
         
    }  
}
