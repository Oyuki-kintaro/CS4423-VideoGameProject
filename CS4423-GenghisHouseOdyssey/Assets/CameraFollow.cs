using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the object you want to follow
    public Vector3 offset; // Offset from the target object

    public float smoothSpeed = 5f;

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position,desiredPosition,smoothSpeed*Time.deltaTime);
        //transform.position = target.transform.position + offset;
    }

}
