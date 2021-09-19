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
        float speedMod = 0.85f;
        if (Input.GetKey(KeyCode.LeftShift)) {
            speedMod = 1.5f;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
            Vector3 movement = transform.right * x + transform.forward * z;
            rb.MovePosition(transform.position + movement.normalized * playerSpeed * speedMod * Time.deltaTime);
        } else {
            rb.MovePosition(transform.position);
        }
    }
}
