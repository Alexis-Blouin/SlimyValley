using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public DynamicInventory inventory;

    public void AddItem(InstanceItemContainer foundItem)
    {
        Debug.Log("item added: " + foundItem.TakeItem().itemType.itemName);
        inventory.AddItem(foundItem.TakeItem());
    }
    
    public void DropItem(int itemIndex)
    {
        // Creates a new object and gives it the item data
        // GameObject droppedItem = new GameObject();
        // droppedItem.AddComponent<Rigidbody>();
        // droppedItem.AddComponent<InstanceItemContainer>().item = inventory.items[itemIndex];
        GameObject itemModel = Instantiate(inventory.items[itemIndex].itemType.droppedModel, Vector3.zero, Quaternion.identity);

        // Removes the item from the inventory
        inventory.items.RemoveAt(itemIndex);

        // Updates the inventory again
        // UpdateInventory();
    }
}
