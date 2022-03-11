using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lookAt();
    }

    // Function for look at for the snake tail
    void lookAt()
    {   
        // Mouse position setup with relative to main camera 
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calculate look at position
        Vector2 lookAtPosition = new Vector2( mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        // Invert the lookat position so the buttom of the object, end of tail looks at mouse instead of the begining 
        transform.up = lookAtPosition * -1.0f;
    }
}
