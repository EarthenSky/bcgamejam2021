using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public MouseItem mouseItem = new MouseItem();
    public GameObject player;
    public GameObject canvas;
    public GameObject inventoryPrefab;
    PlayerStats stats;
    InventoryObject inventory;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int NUMBER_OF_COLUMNS;
    public int Y_SPACE_BETWEEN_ITEMS;
    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot >();


    // Start is called before the first frame update
    void Start()
    {
        inventory = transform.parent.transform.parent.transform.parent.GetComponent<PlayerInventory>().inventory;
        createSlots();
        PlayerStats stats = player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        updateSlots();
    }

    public void createSlots() {

        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = getPosition(i);
            obj.transform.localRotation = Quaternion.identity;
            addEvent(obj, EventTriggerType.PointerEnter, delegate { onEnter(obj); });
            addEvent(obj, EventTriggerType.PointerExit, delegate { onExit(obj); });
            addEvent(obj, EventTriggerType.PointerClick, delegate { onClick(obj); });
            //addEvent(obj, EventTriggerType.BeginDrag, delegate { onDragStart(obj); });
            //addEvent(obj, EventTriggerType.EndDrag, delegate { onDragEnd(obj); });
            //addEvent(obj, EventTriggerType.Drag, delegate { onDrag(obj); });
            itemsDisplayed.Add(obj, inventory.container.Items[i]);
        }

    }

    public Vector3 getPosition(int i) {
        return new Vector3(X_START + X_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_COLUMNS), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i/NUMBER_OF_COLUMNS)), 0f);
    }

    public void updateSlots() {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if (_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.getItem[_slot.Value.item.Id].uiDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            }
            else {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);

            }
        }
        
    }

    private void addEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action) {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    private void onEnter(GameObject obj) { 
        
    }
    private void onExit(GameObject obj)
    {
        
    }

    private void onClick(GameObject obj) {
        ItemType type = inventory.database.getItem[itemsDisplayed[obj].item.Id].type;
        if (type == ItemType.Food)
        {
            stats.Heal(inventory.database.getItem[itemsDisplayed[obj].item.Id].name);
        }
        itemsDisplayed[obj] = new InventorySlot();
        
    }
    /*private void onDragStart(GameObject obj)
    {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        mouseObject.transform.SetParent(transform.parent);
        rt.sizeDelta = new Vector2(40, 40);
        rt.localRotation = Quaternion.identity;
        rt.localScale = new Vector3(1, 1, 1);
        mouseObject.transform.localPosition = Input.mousePosition;
        if (itemsDisplayed[obj].ID >= 0) {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.database.getItem[itemsDisplayed[obj].ID].uiDisplay;
            img.raycastTarget = false;
        }
        mouseItem.obj = mouseObject;
        mouseItem.Item = itemsDisplayed[obj];
    }
    private void onDragEnd(GameObject obj)
    {

    }
    private void onDrag(GameObject obj)
    {
        if (mouseItem != null) {
            mouseItem.obj.transform.position = Input.mousePosition;
        }
    }*/
}

public class MouseItem {
    public GameObject obj;
    public InventorySlot Item;
    public InventorySlot hoverItem;
    public GameObject hoverObj;
}