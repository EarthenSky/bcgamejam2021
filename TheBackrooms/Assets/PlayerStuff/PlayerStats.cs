using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int hp = 4;

    public Sprite face1;
    public Sprite face2;
    public Sprite face3;

    public Sprite current;

    void TakeDamage() {
        hp -= 1;
        if (hp == 3)
            current = face1;
        else if (hp == 2)
            current = face2;
        else if (hp == 3)
            current = face3;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
