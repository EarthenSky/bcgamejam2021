using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;

    float mouseSensitivity = 100f;

    float xRotation = 0f;
    float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
    //---------------------------------Mouse looking stuff--------------------------------
        //getting mouse mouvement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90);

        yRotation += mouseX;


        //doing the transformations on the camera based off of the mouse movement
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
