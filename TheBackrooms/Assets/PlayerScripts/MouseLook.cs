using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private GameObject player;
    public Transform cam;
    public float mouseSensitivity = 100f;

    float xRotation = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
    //---------------------------------Mouse looking stuff--------------------------------
        //getting mouse mouvement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 0.02f;// * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 0.02f;// * Time.deltaTime;

        player.transform.Rotate(0f, mouseX, 0f);

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        //doing the transformations on the camera based off of the mouse movement
        cam.localEulerAngles = new Vector3(xRotation, 0f, 0f);
    }
}
