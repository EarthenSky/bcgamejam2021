using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "new Survival Object", menuName = "Inventory System/Items/Survival")]
public class Survival : ItemObject
{
    private void Awake()
    {
        type = ItemType.Survival;
    }
}
