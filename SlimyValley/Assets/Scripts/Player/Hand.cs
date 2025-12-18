using UnityEngine;

public class Hand : MonoBehaviour
{
    private PlayerInventory _playerInventory;
    private SpriteRenderer _spriteRenderer;
    
    public int activeHandItem = 0;
    
    void Start()
    {
        _playerInventory = GetComponentInParent<PlayerInventory>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateHandSprite()
    {
        if (_playerInventory.HasIndex(activeHandItem))
        {
            _spriteRenderer.sprite = _playerInventory.GetSpriteIndex(activeHandItem);
        }
        else
        {
            _spriteRenderer.sprite = null;
        }
    }
}
