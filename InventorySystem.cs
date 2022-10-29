using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public List<InventorySlot> slots;       //All slots including player stat slots
    List<InventorySlot> regularSlots;       //Regular slots (not head, chest, leg or hand slot)

    public List<Item_SO> items;

    bool fullInventory = false;

    private void Start()
    {
        InventorySlot[] allSlots = FindObjectsOfType<InventorySlot>(true);      //Find all objects in scene.

        regularSlots = new List<InventorySlot>();           //Create new list

        foreach (InventorySlot invs in slots)
        {
            if (invs.slotType == InvSlotType.regular)       //Find all regular slots
            {
                regularSlots.Add(invs);                     //Add them to the regularSlots list
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            AddItem(0, 12);
        }
    }

    public void AddItem(int itemID, int addAmount)          //Function for adding items to inventory.
    {
        if (fullInventory) return;                          //If inventory is full we will break the function;

        //InventorySlot reservedSlot;

        for (int i = 0; i < regularSlots.Count; i++)
        {
            InventorySlot currentTestSlot = regularSlots[i];        //This is done for easier code writing.

            if (currentTestSlot.heldItem == null)                   //Here we check if the slot[i] is holding item or is free space.
            {
                currentTestSlot.SetItem(FindItemByID(itemID), addAmount);                             //If so then we return that slot.
                break;
            }
            else
            {
                if (currentTestSlot.heldItem.item_ID == itemID)     //We check if item that we are adding is the same that is held in that slot if so we can maybe add it there, we proceed.
                {
                    if (currentTestSlot.currentStack == currentTestSlot.heldItem.item_maxStack)
                    {
                        continue;
                    }

                    if (currentTestSlot.currentStack + addAmount <
                        currentTestSlot.heldItem.item_maxStack)     //Calculating if the amount of that item in this slot will go over it's max stack. If not...
                    {
                        currentTestSlot.AddStack(addAmount);
                        break;
                    }
                    else
                    {
                        int overflowAmount = currentTestSlot.currentStack + addAmount - 
                                             currentTestSlot.heldItem.item_maxStack;                //Total amount of items to be added to next slot.

                        int amountForCurrentSlot = currentTestSlot.heldItem.item_maxStack - 
                                                   currentTestSlot.currentStack;                    //Total amount of items to be added to this slot.

                        currentTestSlot.AddStack(amountForCurrentSlot);                             //Adding to this slot.

                        if (i == regularSlots.Count - 1)
                        {
                            if (currentTestSlot.currentStack >= currentTestSlot.heldItem.item_maxStack)
                            {
                                fullInventory = true;
                            }
                        }

                        if (overflowAmount > 0) AddItem(itemID, overflowAmount);                    //Adding items somewhere else.      !!IMPORTANT - if inv is full destroy the items, or drop on ground
                        break;
                    }
                }
                else
                {
                    continue;
                }
            }
        }
    }

    Item_SO FindItemByID(int id)                            //A way to fing according scriptable object item by it's ID.
    {
        Item_SO itm = null;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item_ID == id)
            {
                itm =  items[i];
                break;
            }
            else
            {
                continue;
            }
        }

        return itm;
    }
}
