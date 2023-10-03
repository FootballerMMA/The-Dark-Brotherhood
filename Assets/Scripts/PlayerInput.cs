using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PInpuy : MonoBehaviour
{
    Movement movement;
    void Awake(){
        movement = GetComponent<Movement>();
    }
    void FixedUpdate(){
        Vector3 vel = Vector3.zero;
        if (Input.GetKey(KeyCode.W)){
            vel.y = 1;
        }
        if (Input.GetKey(KeyCode.S)){
            vel.y = -0.5f;
        }
        if (Input.GetKey(KeyCode.A)){
            vel.x = -1;
        }
        if (Input.GetKey(KeyCode.D)){
            vel.x = 1;
        }
        
        movement.MoveRigidbody(vel);
    }
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space)){
            movement.ToggleFly();
        }
        */
    }
    public void clickimage(){
        Debug.Log ("Clicked card");
    }
}
