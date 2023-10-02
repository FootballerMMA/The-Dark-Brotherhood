using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float FollowSpeed = 2f;
    [SerializeField] Transform target;
    private float yOffset = 3.5f;
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset,-10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed*Time.deltaTime);
    }
}
