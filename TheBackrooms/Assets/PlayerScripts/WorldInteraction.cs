using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInteraction : MonoBehaviour
{

    public GameObject camObject;
    Camera cam;
    PlayerInventory inv;


    bool showingText = false;
    private void Start()
    {
        cam = camObject.GetComponent<Camera>();
        inv = GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        Ray interactionRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity)) {
            GameObject interactedObject = interactionInfo.collider.gameObject;
            if (interactedObject.tag == "Item")
            {
                Item item = interactedObject.GetComponent<Item>();
                if (!showingText)
                {
                    item.showFloatingText();
                    showingText = true;
                }
         

                if (Input.GetKey(KeyCode.F))
                {
                    inv.inventory.addItem(item.item);
                    Destroy(interactedObject.gameObject);
                }
                //have text appear above item saying "press f to pickup"
                //wait for f to be pressed and add to inventory
            }
        }
    }

    
}
