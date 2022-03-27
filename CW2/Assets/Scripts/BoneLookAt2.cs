using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneLookAt2 : MonoBehaviour
{
    [SerializeField] public GameObject prevBone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

// Update is called once per frame
    void Update()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg/5*3;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //Debug.Log(GameObject.Find("bone_5").transform.position);
        //Debug.Log("prev:"+prevBone.dir.y +"   "+prevBone.dir.x);

    }


}
