using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public float amplitude = 2f;
    public float speed = 1.5f;
    public float adjuster = 150f;
    void Update()
    {
        Vector3 p = transform.position;
        p.y = amplitude * Mathf.Cos(Time.time * speed) + adjuster;
        transform.position = p;
    }
}
