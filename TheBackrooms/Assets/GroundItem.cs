using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : MonoBehaviour
{
    public GameObject textPrefab;
    public ItemObject item;
    GameObject player;
    GameObject text;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (text != null)
        {
            lookTextAtPlayer();
        }
    }   

    public void showFloatingText()
    {
        text = Instantiate(textPrefab, transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity, transform);
    }

    public void lookTextAtPlayer() {
        text.transform.LookAt(player.transform);
        text.transform.Rotate(new Vector3(0f, 180f, 0f));
    }

}
