using System;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private Hand hand;
    
    private PlayerInventory _playerInventory;

    private void Start()
    {
        _playerInventory = GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out InstanceItemContainer foundItem))
        {
            if (foundItem.CanTakeItem())
            {
                _playerInventory.AddItem(foundItem);
                hand.UpdateHandSprite();
            }
        }
    }
}
