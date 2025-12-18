using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public DynamicInventory baseInventory;

    private DynamicInventory _runtimeInventory;

    private void Start()
    {
        _runtimeInventory = Instantiate(baseInventory);
        _runtimeInventory.Init();
    }

    public bool HasIndex(int index)
    {
        return _runtimeInventory.HasIndex(index);
    }

    public bool CanPlaceIndex(int index)
    {
        return _runtimeInventory.CanPlaceIndex(index);
    }

    public GameObject GetIndex(int index)
    {
        return _runtimeInventory.GetIndex(index);
    }
    
    public GameObject GetPlaceIndex(int index)
    {
        return _runtimeInventory.GetPlaceIndex(index);
    }

    public void AddItem(InstanceItemContainer foundItem)
    {
        _runtimeInventory.AddItem(foundItem.TakeItem());
    }
    
    public void DropItem(int itemIndex, Vector2 direction)
    {
        if (!_runtimeInventory.HasIndex(itemIndex))
            return;
        // Creates a new object and gives it the item data
        // GameObject droppedItem = new GameObject();
        // droppedItem.AddComponent<Rigidbody>();
        // droppedItem.AddComponent<InstanceItemContainer>().item = _runtimeInventory.items[itemIndex];
        var itemModel = Instantiate(_runtimeInventory.items[itemIndex].itemType.droppedModel, transform.position, Quaternion.identity);
        itemModel.GetComponent<InstanceItemContainer>().MoveOndrop(direction);

        // Removes the item from the inventory
        _runtimeInventory.LowerIndex(itemIndex);

        // Updates the inventory again
        // UpdateInventory();
    }

    public void PlaceItem(int itemIndex)
    {
        _runtimeInventory.LowerIndex(itemIndex);
    }
}
