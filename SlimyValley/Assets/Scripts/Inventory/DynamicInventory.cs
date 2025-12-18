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
        if (itemToAdd == null || itemToAdd.itemType == null)
        {
            Debug.LogError("Trying to add invalid ItemInstance");
            return false;
        }
        
        // Try stacking first
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null &&
                items[i].itemType == itemToAdd.itemType &&
                items[i].count < itemToAdd.itemType.maxCount)
            {
                items[i].count += itemToAdd.count;
                return true;
            }
        }

        // Then find empty slot
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd;
                return true;
            }
        }

        Debug.Log("No space in the inventory");
        return false;
    }
    
    public bool HasIndex(int index)
    {
        return index >= 0 &&
               index < items.Count &&
               items[index] != null;
    }

    public bool CanPlaceIndex(int index)
    {
        return HasIndex(index) && items[index].itemType.placable;
    }

    public GameObject GetIndex(int index)
    {
        return items[index].itemType.droppedModel;
    }

    public Sprite GetSpriteIndex(int index)
    {
        return items[index].itemType.icon;
    }
    
    public GameObject GetPlaceIndex(int index)
    {
        return items[index].itemType.placedModel;
    }

    public void LowerIndex(int index)
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
