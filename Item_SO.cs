using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "InventorySystem/Item", order = 1)]
public class Item_SO : ScriptableObject
{
    public string item_name;                //Name of the item
    public int item_ID;                     //Identification for item
    public Sprite item_image;               //Sprite that will be shown in inventory
    public GameObject item_prefab;          //Actual in game prefab of the item
    public int item_maxStack;               //Max stacks for this item;
}
