using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 1f;

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        if (target.transform.position.y -transform.position.y < -6.15) 
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 newPos = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = newPos;
        }
    }
}
