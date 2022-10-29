using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum InvSlotType
{
    regular,
    hand,
    leg,
    head,
    chest,
}

public class InventorySlot : MonoBehaviour
{
    public InvSlotType slotType;

    public TextMeshProUGUI stackText;
    public Image itemSprite;
    [Space]
    //public Color nullItemColor;
    //[Space]
    public Item_SO heldItem;
    public int currentStack = 0;

    private void Awake()
    {
        RefreshUI();
    }

    public void SetItem(Item_SO item, int stack)            // Here we remove the old held item and replace it with new
    {                                                       // one and it's stack
        heldItem = item;                                     
        currentStack = stack;

        RefreshUI();
    }                                                        

    public void AddStack(int amount)                        // We use this function to add stack to this slot(Add more items)
    {
        currentStack += amount;

        RefreshUI();
    }
    public void RemoveItem()                                // Used to remove the whole content of inv. slot.
    {
        heldItem = null;
        currentStack = 0;
        RefreshUI();
    }
    public void RemoveStack(int amount)                     // Used to remove specific amount of items from inventory slot.
    {
        if (currentStack - amount == 0)
        {
            RemoveItem();
            return;
        }
        else if (currentStack - amount < 0)
        {
            int nonflow = amount - currentStack;            // Amount of items that do not exist.
            print("Non-existing: " + nonflow);

            RemoveItem();
        }
        else
        {
            currentStack -= amount;
        }

        RefreshUI();
    }

    void RefreshUI()                                        // Refresh the UI
    {
        if (heldItem == null)
        {
            itemSprite.sprite = null;
            stackText.text = "";
        }
        else
        {
            itemSprite.sprite = heldItem.item_image;

            stackText.text = $"{currentStack}";

            if (heldItem.item_maxStack == 1)
            {
                stackText.text = "";
            }
        }
    }
}
