using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public DynamicInventory inventory;

    private void Start()
    {
        inventory.Init();
    }

    public bool HasIndex(int index)
    {
        return inventory.HasIndex(index);
    }

    public bool CanPlaceIndex(int index)
    {
        return inventory.CanPlaceIndex(index);
    }

    public GameObject GetIndex(int index)
    {
        return inventory.GetIndex(index);
    }
    
    public GameObject GetPlaceIndex(int index)
    {
        return inventory.GetPlaceIndex(index);
    }

    public void AddItem(InstanceItemContainer foundItem)
    {
        inventory.AddItem(foundItem.TakeItem());
    }
    
    public void DropItem(int itemIndex, Vector2 direction)
    {
        if (!inventory.HasIndex(itemIndex))
            return;
        // Creates a new object and gives it the item data
        // GameObject droppedItem = new GameObject();
        // droppedItem.AddComponent<Rigidbody>();
        // droppedItem.AddComponent<InstanceItemContainer>().item = inventory.items[itemIndex];
        var itemModel = Instantiate(inventory.items[itemIndex].itemType.droppedModel, transform.position, Quaternion.identity);
        itemModel.GetComponent<InstanceItemContainer>().MoveOndrop(direction);

        // Removes the item from the inventory
        inventory.DropIndex(itemIndex);

        // Updates the inventory again
        // UpdateInventory();
    }
}
