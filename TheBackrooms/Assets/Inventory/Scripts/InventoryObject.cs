using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenuAttribute(fileName = "new Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public ItemDatabaseObject database;
    public Inventory container;

    public void addItem(Item _item) {

        
        
        
    }

    public InventorySlot setEmptySlot(Item _item) {
        for (int i = 0; i < container.Items.Length; i++)
        {
            if (container.Items[i].ID <= -1) {
                container.Items[i].updateSlot(_item.Id,_item);
                return container.Items[i];
            }
            
        }
        return null;
    }

    public void clearInv() {
        container.Items = new InventorySlot[20];
    }

  
}


[System.Serializable]
public class InventorySlot {
    public int ID = -1;
    public Item item;
    //TODO: ADD (slotsize)
    public InventorySlot()
    {
        item = null;
        ID = -1;

    }

    public InventorySlot(int _id, Item _item) {
        item = _item;
        ID = _id;
        
    }

    public void updateSlot(int _id, Item _item)
    {
        item = _item;
        ID = _id;

    }

}

[System.Serializable]
public class Inventory {
    public InventorySlot[] Items = new InventorySlot[20];

}
