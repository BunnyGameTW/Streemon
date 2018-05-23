using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public float testspeed = 0.25f;
    float interpVelocity;
    public GameObject target;
    public Vector3 offset;
    Vector3 targetPos;
    public Transform LB, RT;
    // Use this for initialization
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {    
        Vector3 posNoZ = transform.position;
        posNoZ.z = target.transform.position.z;
        Vector3 targetDirection = (target.transform.position - posNoZ);
        interpVelocity = targetDirection.magnitude * 5f;
        targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.fixedDeltaTime);
        transform.position = Vector3.Lerp(transform.position, targetPos + offset, testspeed);
        if (transform.position.x < LB.position.x) transform.position = new Vector3(LB.position.x, transform.position.y, transform.position.z);
        else if (transform.position.x > RT.position.x) transform.position = new Vector3(RT.position.x,transform.position.y, transform.position.z);
        if (transform.position.y < LB.position.y) transform.position = new Vector3(transform.position.x, LB.position.y, transform.position.z);
        else if (transform.position.y > RT.position.y) transform.position = new Vector3(transform.position.x, RT.position.y, transform.position.z);

    }
   
}
