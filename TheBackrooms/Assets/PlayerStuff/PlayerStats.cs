using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int hp = 4;

    public Sprite face1;
    public Sprite face2;
    public Sprite face3;
    public Sprite dead;

    public Sprite current;

    private GameObject overlay;

    void TakeDamage() {
        hp -= 1;
        if (hp == 3)
            current = face1;
        else if (hp == 2)
            current = face2;
        else if (hp == 1)
            current = face3;
        else if (hp == 0)
            current = dead;

        overlay.GetComponent<SpriteRenderer>().sprite = current;

        if (hp == 0) {
            // TODO: end the timer
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        overlay = new GameObject("overlay");
        overlay.AddComponent<SpriteRenderer>();
        overlay.transform.parent = transform.Find("Camera").transform;
        overlay.transform.localPosition = new Vector3(0, 0, 0.31f);
        overlay.transform.localScale = new Vector3(0.041f, 0.041f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) {
            TakeDamage();
        }
    }
}
