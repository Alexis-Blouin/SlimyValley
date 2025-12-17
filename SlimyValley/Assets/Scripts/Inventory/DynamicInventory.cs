using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DynamicInventory : ScriptableObject
{
    public int maxItems = 10; 
    public List<ItemInstance> items = new();

    public void Init()
    {
        items.Clear();
        for (int i = 0; i < maxItems; i++)
            items.Add(null);
    }

    public bool AddItem(ItemInstance itemToAdd)
    {
        // Finds an empty slot if there is one
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemType == null)
            {
                items[i] = itemToAdd;
                return true;
            }
            // If the item is there and that is not at max, we can increment it
            if (items[i].itemType == itemToAdd.itemType && items[i].count < itemToAdd.itemType.maxCount)
            {
                items[i].count += itemToAdd.count;
                return true;
            }
        }

        // Adds a new item if the inventory has space
        // if (items.Count < maxItems)
        // {
        //     items.Add(itemToAdd);
        //     return true;
        // }

        Debug.Log("No space in the inventory");
        return false;
    }
    
    public bool HasIndex(int index)
    {
        return items[index].itemType != null;
    }

    public bool CanPlaceIndex(int index)
    {
        return items[index].itemType.placable;
    }

    public GameObject GetIndex(int index)
    {
        return items[index].itemType.droppedModel;
    }
    
    public GameObject GetPlaceIndex(int index)
    {
        return items[index].itemType.droppedModel;
    }

    public void DropIndex(int index)
    {
        if (items[index].count > 1)
        {
            items[index].count -= 1;
        }
        else
        {
            items[index] = null;
        }
    }
}
