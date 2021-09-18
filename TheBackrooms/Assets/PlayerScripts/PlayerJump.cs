using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    public float jumpVelocity = 4f;
    public float fallMultiplier = 2.5f;

    public Transform groundCheck;
    float groundDistance = 1f;
    public LayerMask groundLayer;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {   
        //gives a crisper jump
        if (rb.velocity.y < 0) {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        if (isGrounded() && Input.GetKey(KeyCode.Space)){
            rb.velocity = Vector3.up * jumpVelocity;
        }
    }

    bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
    }


}
