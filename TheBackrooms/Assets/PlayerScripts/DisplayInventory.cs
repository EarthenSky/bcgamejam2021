using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public GameObject inventoryPrefab;
    public InventoryObject inventory;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int NUMBER_OF_COLUMNS;
    public int Y_SPACE_BETWEEN_ITEMS;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        createDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        updateDisplay();
    }

    public void createDisplay() {
        for (int i = 0; i < inventory.container.Items.Count; i++) {
            InventorySlot slot = inventory.container.Items[i];
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.getItem[slot.ID].uiDisplay;
            obj.GetComponent<RectTransform>().localPosition = getPosition(i);
            itemsDisplayed.Add(slot, obj);
        }
    }

    public Vector3 getPosition(int i) {
        return new Vector3(X_START + X_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_COLUMNS), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i/NUMBER_OF_COLUMNS)), 0f);
    }

    public void updateDisplay() {
        for (int i = 0; i < inventory.container.Items.Count; i++) {
            InventorySlot slot = inventory.container.Items[i];
            if (!itemsDisplayed.ContainsKey(inventory.container.Items[i])) {
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.getItem[slot.ID].uiDisplay;
                obj.GetComponent<RectTransform>().localPosition = getPosition(i);
                itemsDisplayed.Add(slot, obj);
            }

        }
    }
}
