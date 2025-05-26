using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(0.1f, 1f)]
    public float smoothness;

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, smoothness) + offset;
    }
}
