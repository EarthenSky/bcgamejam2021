using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject textPrefab;
    public ItemObject item;
    public GameObject player;
    GameObject text;

    private void Update()
    {
        if (text != null)
        {
            lookTextAtPlayer();
        }
    }   

    public void showFloatingText()
    {
        text = Instantiate(textPrefab, transform.position, Quaternion.identity, transform);
    }

    public void lookTextAtPlayer() {
        text.transform.LookAt(player.transform);
        text.transform.Rotate(new Vector3(0f, 180f, 0f));
    }

}
