using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//adds to asset menu
[CreateAssetMenuAttribute(fileName = "new Default Object", menuName = "Inventory System/Items/Default")]
public class DefaultItem : ItemObject
{
    private void Awake()
    {
       type = ItemType.Default;
        
    }
}
