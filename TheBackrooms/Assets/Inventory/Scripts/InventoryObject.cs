using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenuAttribute(fileName = "new Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> container = new List<InventorySlot>();

    public void addItem(ItemObject _item) {

        InventorySlot itemSlot = new InventorySlot(_item);
        container.Add(itemSlot);
        
    }
}


[System.Serializable]
public class InventorySlot {

    public ItemObject item;
    //TODO: ADD (slotsize)

    public InventorySlot(ItemObject _item) {
        item = _item;    
    }

}
