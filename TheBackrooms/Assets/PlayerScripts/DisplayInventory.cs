using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public GameObject canvas;
    public GameObject inventoryPrefab;
    public InventoryObject inventory;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int NUMBER_OF_COLUMNS;
    public int Y_SPACE_BETWEEN_ITEMS;
    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot >();


    // Start is called before the first frame update
    void Start()
    {
        createSlots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createSlots() {

        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = getPosition(i);
            obj.transform.localRotation = Quaternion.identity;
            itemsDisplayed.Add(obj, inventory.container.Items[i]);
        }

    }

    public Vector3 getPosition(int i) {
        return new Vector3(X_START + X_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_COLUMNS), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i/NUMBER_OF_COLUMNS)), 0f);
    }

    public void updateDisplay() {
        
    }
}
