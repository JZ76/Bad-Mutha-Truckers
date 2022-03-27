using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerFixed : MonoBehaviour
{
    public float health = 100f;
    
    public Transform healthBar;
    
    public float radius = 25f;
    
    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;
    
    public bool canSeePlayer;
    
    Transform target;
    
    public Transform firePoint;     // fire point 
    
    public GameObject bulletPrefab; // bullet prefab
    
    public float bulletSpeed = 20;  // bullet speed

    public float fireRate = 0.5f;   // fire rate
    
    public float rotationSpeed = 50;
    
    float nextShotTime = 0;     // next shot time

    public bool toggleShoot = false;    // shooting toggle

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (canSeePlayer)
        {
            Vector3 direction = playerRef.transform.position - transform.position;
            float step = rotationSpeed * Time.deltaTime;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
            shoot();
        }
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void FieldOfViewCheck()
    {
        Collider2D rangeChecks = Physics2D.OverlapCircle(new Vector2(transform.position.x,transform.position.y), radius, targetMask);

        if (rangeChecks != null)
        {
            target = rangeChecks.transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            canSeePlayer = !Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask);
        }
        else
        {
            canSeePlayer = false;
        }
    }
    // Shoot function
    void shoot (){
        // only shoot when time reached
        if (nextShotTime < Time.time) 
        {
            nextShotTime = fireRate + Time.time;    // set next shot time

            // Create bullet object from prefab at fire point with rigidbody, force, speed, rotation
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.AddForce(firePoint.up* -1.0f * bulletSpeed, ForceMode2D.Impulse);
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        canSeePlayer = true;
        healthBar.localScale = new Vector3(health/30, 1f);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
