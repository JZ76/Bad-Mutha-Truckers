using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneLookAt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

// Update is called once per frame
    void Update()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Debug.Log(angle);
        // lookAt();
    }

    // Function for look at for the snake tail
    // void lookAt()
    // {   
    //     // Mouse position setup with relative to main camera 
    //     Vector3 mousePosition = Input.mousePosition;
    //     mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

    //     // Calculate look at position
    //     Vector2 lookAtPosition = new Vector2( mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

    //     // Invert the lookat position so the buttom of the object, end of tail looks at mouse instead of the begining 
    //     transform.up = lookAtPosition * 0.5f;
    //     Debug.Log(lookAtPosition * -1.0f);
    // }
}
