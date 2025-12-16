using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public DynamicInventory inventory;

    public void AddItem(InstanceItemContainer foundItem)
    {
        Debug.Log("item added: " + foundItem.TakeItem().itemType.itemName);
        inventory.AddItem(foundItem.TakeItem());
    }
    
    public void DropItem(int itemIndex, Vector2 direction)
    {
        // Creates a new object and gives it the item data
        // GameObject droppedItem = new GameObject();
        // droppedItem.AddComponent<Rigidbody>();
        // droppedItem.AddComponent<InstanceItemContainer>().item = inventory.items[itemIndex];
        var itemModel = Instantiate(inventory.items[itemIndex].itemType.droppedModel, transform.position, Quaternion.identity);
        itemModel.GetComponent<InstanceItemContainer>().MoveOndrop(direction);

        // Removes the item from the inventory
        inventory.items.RemoveAt(itemIndex);

        // Updates the inventory again
        // UpdateInventory();
    }
}
