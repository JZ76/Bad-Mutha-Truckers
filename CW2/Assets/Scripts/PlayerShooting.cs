using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject player;
    public Transform firePoint;     // fire point 
    public Transform firePoint2;     // fire point 
    public Transform firePoint3;     // fire point 
    public GameObject bulletPrefab; // bullet prefab
    public float bulletSpeed = 20;  // bullet speed

    public float fireRate = 0.5f;   // fire rate

    bool fireMode1 = true;
    bool fireMode2 = false;
    bool fireMode3 = false;

    int count = 0;
    void Start()
    {
        Debug.Log("start");
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Count each hit
        if (collision.gameObject.name == "Bullet(Clone)" && fireMode1)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.AddForce(firePoint.up* -1.0f * bulletSpeed, ForceMode2D.Impulse);
        }

        if (collision.gameObject.name == "Bullet(Clone)" && fireMode2)
        {
            count++;
        }
        PlayerMovement p = player.GetComponent<PlayerMovement>();
        p.health = Math.Min(p.health + 10, 100);
        if (collision.gameObject.name == "Bullet(Clone)" && fireMode3)
        {
            p.health += 10;
        }
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            fireMode1 = true;
            fireMode2 = false;
            fireMode3 = false;
            count = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            fireMode1 = false;
            fireMode2 = true;
            fireMode3 = false;

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            fireMode1 = false;
            fireMode2 = false;
            fireMode3 = true;
            count = 0;
        }

        if (Input.GetMouseButtonDown(0) && fireMode2 && count>0)
            {
                count--;
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                bulletRigidbody.AddForce(firePoint.up* -1.0f * bulletSpeed, ForceMode2D.Impulse);
            }

        if (Input.GetMouseButtonDown(1) && fireMode2 && count>2)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.AddForce(firePoint.up* -1.0f * bulletSpeed, ForceMode2D.Impulse);

            GameObject bullet2 = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
            Rigidbody2D bulletRigidbody2 = bullet2.GetComponent<Rigidbody2D>();
            bulletRigidbody2.AddForce(firePoint2.up* -1.0f * bulletSpeed, ForceMode2D.Impulse);

            GameObject bullet3 = Instantiate(bulletPrefab, firePoint3.position, firePoint3.rotation);
            Rigidbody2D bulletRigidbody3 = bullet3.GetComponent<Rigidbody2D>();
            bulletRigidbody3.AddForce(firePoint3.up* -1.0f * bulletSpeed, ForceMode2D.Impulse);


            count = 0;
        }
    }
}
