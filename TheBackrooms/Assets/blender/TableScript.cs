using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableScript : MonoBehaviour
{
    private GameObject player;
    private int frame;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360)));
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // check once per 30 frames
        if (frame % 30 == 0) {
            if (Vector3.Distance(player.transform.position, transform.position) > 500) {
                Destroy(gameObject); // THIS OBJECT WILL die eventually.
            }       
        }

        frame++;
    }
}
