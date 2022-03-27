using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform healthBar;
    public float health = 100f;
    Vector2 movementInput;
    public float movementSpeed;
    public float rotationSpeed;

    Rigidbody2D playerRigidbody;
    // Start is called before the first frame update
    void Start()
    {
         playerRigidbody = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        // Movement coordinates for x and y
        movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Move the transform using movement input relative to world space
        transform.Translate(movementInput * movementSpeed * Time.deltaTime, Space.World);

        // Check if movement input is pressed
        if (movementInput != Vector2.zero){

            // Store target rotation looking at z axis and movement input as up
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, movementInput);

            // Rotate transform to the target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed*Time.deltaTime);
        }
    
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.localScale = new Vector3(health/100, 1f);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
