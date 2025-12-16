using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject attackNode;
    [SerializeField] private Vector2 attackOffset;
    [SerializeField] private GameObject inHandObject;

    private Vector2 _direction;
    
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    private PlayerInventory _playerInventory;

    private AttackArea _attackBoxCollider;

    private bool _isPlacing = false;
    private GameObject _placingObject;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        _playerInventory = GetComponent<PlayerInventory>();

        _attackBoxCollider = attackNode.GetComponent<AttackArea>();
    }

    void Update()
    {
        transform.Translate(_direction * (speed * Time.deltaTime), Space.World);

        if (_isPlacing)
        {
            Vector3 mouseScreenPos = Mouse.current.position.ReadValue();

            Vector3 mouseWorld =
                Camera.main.ScreenToWorldPoint(mouseScreenPos);
            mouseWorld.z = 0f;
            
            _placingObject.transform.position = mouseWorld;
        }
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();
        _animator.SetBool("Xmov", _direction.x is > 0.0f or < 0.0f);
        _animator.SetFloat("Ymov", _direction.y);
        
        if (_direction.x != 0.0f)
        {
            if (_direction.x < 0.0f)
            {
                _spriteRenderer.flipX = true;
            }
            else if (_direction.x > 0.0f)
            {
                _spriteRenderer.flipX = false;
            }
        }

        if (_direction != Vector2.zero)
        {
            attackNode.transform.position = transform.position + new Vector3(attackOffset.x * _direction.x, attackOffset.y * _direction.y, attackNode.transform.position.z);
        }
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_isPlacing)
            {
                _isPlacing = false;
            }
            else
            {
                _attackBoxCollider.DoAttack();
            }
        }
    }

    public void OnItem1Equipped(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        _placingObject = Instantiate(inHandObject);
        _isPlacing = true;
    }

    public void OnDropItem(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        _playerInventory.DropItem(0);
    }
}
