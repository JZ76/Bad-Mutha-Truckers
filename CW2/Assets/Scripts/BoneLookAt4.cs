using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneLookAt4 : MonoBehaviour
{
    [SerializeField] public GameObject prevBone;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("SnakeBody").transform.localEulerAngles.z > 180)
        {
            Debug.Log(GameObject.Find("SnakeBody").transform.localEulerAngles.z);
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg / 5;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {

            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg / 5;
            Debug.Log(angle);
        };


    }


}
