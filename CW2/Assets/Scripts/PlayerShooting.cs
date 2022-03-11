using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

 public Transform firePoint;     // fire point 
    public GameObject bulletPrefab; // bullet prefab
    public float bulletSpeed = 20;  // bullet speed

    public float fireRate = 0.5f;   // fire rate
    void Start()
    {
        Debug.Log("start");
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Count each hit
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.AddForce(firePoint.up* -1.0f * bulletSpeed, ForceMode2D.Impulse);
        }
    }
}
