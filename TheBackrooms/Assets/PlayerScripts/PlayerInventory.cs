using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public GameObject canvas;
    public GameObject lookObject;


    bool shown = false;
    private void Awake()
    {
        inventory.clearInv();
        canvas.SetActive(false);
    }

    private void Update()
    {   
        //toggle inventory
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (shown)
            {
                canvas.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                lookObject.SetActive(true);
                shown = false;
                
            }
            else {
                canvas.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                lookObject.SetActive(false);
                shown = true;
            }
        }
    }
}
