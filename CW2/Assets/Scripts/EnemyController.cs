using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float health = 100f;
    
    
    public float radius = 25f;
    
    public GameObject playerRef;

    [Range(0, 360)] public float angle = 60f;
    
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    
    public bool canSeePlayer;

    public float speed = 2;
    
    public float rotationSpeed = 1;
    
    Vector2[] path;
    
    int targetIndex;
    
    Transform target;
    // Shooting variables 
    public Transform firePoint;     // fire point 
    public GameObject bulletPrefab; // bullet prefab
    public float bulletSpeed = 20;  // bullet speed

    public float fireRate = 0.5f;   // fire rate
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
            shoot();
        }
    }
    
    public void OnPathFound(Vector2[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector2 currentWaypoint = path[0];
        while (true)
        {
            if (new Vector2(transform.position.x, transform.position.y) == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }

                currentWaypoint = path[targetIndex];
            }

            // face towards next waypoint
            Vector2 targetDir = currentWaypoint - new Vector2(this.transform.position.x, transform.position.y);
            float step = this.rotationSpeed * Time.deltaTime;
            
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, newDir);
            
            // move towards next waypoint
            transform.position = Vector2.MoveTowards(this.transform.position, 
                currentWaypoint, 
                this.speed * Time.deltaTime);
            yield return null;
        }
    }
    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                // draw brick on Gizmos mode for debugging
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);
                
                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            if (canSeePlayer)
            {
                PathManager.RequestPath(new Vector2(transform.position.x, transform.position.y),
                    new Vector2(target.position.x, target.position.y), this, OnPathFound);
            }
            else
            {
                if (health < 100f)
                {
                    PathManager.RequestPath(new Vector2(transform.position.x, transform.position.y),
                        new Vector2(playerRef.transform.position.x, playerRef.transform.position.y), this, OnPathFound);
                }
            }

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

            if (Vector2.Angle(transform.up, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);
                canSeePlayer = !Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask);
            }
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
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
