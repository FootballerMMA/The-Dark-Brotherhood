using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //[SerializeField] private float speed = 20f;
    private float speed = 4f;
    Rigidbody2D rb;
    float gravity;
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale;
    }
    /*
    public void MoveTransform(Vector3 vel){
        transform.position += vel * Time.deltaTime * speed;
    }
    */
    public void MoveRigidbody(Vector3 vel){
        rb.MovePosition(transform.position + (vel * Time.fixedDeltaTime * speed));
    }
    /*
    public void ToggleFly(){
        if (gravity == 0.0f){
            rb.gravityScale = 1f;
            gravity = 1f;
        } else {
            rb.gravityScale = 0.0f;
            gravity = 0.0f;
        }
    }
    */
}
