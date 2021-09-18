using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//adds to asset menu
[CreateAssetMenuAttribute(fileName = "new Food Object", menuName = "Inventory System/Items/Food")]
public class FoodObject : ItemObject
{
    public int restoreHealthValue;
    private void Awake()
    {
        type = ItemType.Food;
        
    }
}
