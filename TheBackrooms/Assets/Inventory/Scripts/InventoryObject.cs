using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenuAttribute(fileName = "new Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public ItemDatabaseObject database;
    public Inventory container;

    public void addItem(Item _item) {

        InventorySlot itemSlot = new InventorySlot(_item.Id,_item);
        container.Items.Add(itemSlot);
        
    }

    public void clearInv() {
        container.Items.Clear();
    }

  
}


[System.Serializable]
public class InventorySlot {
    public int ID;
    public Item item;
    //TODO: ADD (slotsize)

    public InventorySlot(int _id, Item _item) {
        item = _item;
        ID = _id;
        
    }

}

[System.Serializable]
public class Inventory {
    public List<InventorySlot> Items = new List<InventorySlot>();

}
