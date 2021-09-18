using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "new Crafting Object", menuName = "Inventory System/Items/Crafting")]
public class CraftingObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Crafting;
    }

}
