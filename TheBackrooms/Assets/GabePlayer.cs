using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GabePlayer : MonoBehaviour
{
    public float speed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) {
            moveDir = new Vector3(0, 0, 1) * speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.A)) {
            moveDir = new Vector3(-1, 0, 0) * speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.S)) {
            moveDir = new Vector3(0, 0, -1) * speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.D)) {
            moveDir = new Vector3(1, 0, 0) * speed * Time.deltaTime;
        }
        transform.Translate(moveDir);
    }
}
