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
        Vector3 movement = transform.right * x + transform.forward * z;
        rb.MovePosition(transform.position + movement.normalized * playerSpeed * Time.deltaTime);
    }
}
