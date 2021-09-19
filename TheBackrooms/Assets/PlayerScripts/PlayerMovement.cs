using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 12f;

    Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frrbame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        //creating the transformation vector and applying the movement to ridgidbody
        //rb.MovePosition(transform.position + movement.normalized * playerSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
            Vector3 movement = transform.right * x + transform.forward * z;
            Vector3 final = movement.normalized * playerSpeed * 20 * Time.deltaTime;
            rb.velocity = new Vector3(final.x, rb.velocity.y, final.z);
        } else {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
}
