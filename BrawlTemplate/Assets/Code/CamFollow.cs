using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    // put insdie of the player game object
    // it will detach from parent at start
    public Transform target;
    public float smoothSpeed = 10f;
    public Vector3 offset;
    public Vector3 velocity = Vector3.one;


    public void Start()
    {
        
        transform.parent = null;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPostion = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPostion,ref velocity,smoothSpeed * Time.deltaTime);
       
        transform.position = smoothedPosition ;

        //transform.LookAt(target);

    }
}
