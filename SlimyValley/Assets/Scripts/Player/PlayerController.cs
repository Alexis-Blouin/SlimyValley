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
    private Directions _currentDirection;
    
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    private PlayerInventory _playerInventory;

    private AttackArea _attackBoxCollider;

    private bool _isPlacing = false;
    private GameObject _placingObject;
    private int _activeHandItem = 0;

    private enum Directions
    {
        Top,
        Bottom,
        Left,
        Right,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }
    
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

            if (_direction is { x: < 0.0f, y: 0.0f })
            {
                _currentDirection = Directions.Left;
            } else if (_direction is { x: > 0.0f, y: 0.0f })
            {
                _currentDirection = Directions.Right;
            }else if (_direction is { x: 0.0f, y: < 0.0f })
            {
                _currentDirection = Directions.Bottom;
            }else if (_direction is { x: 0.0f, y: > 0.0f })
            {
                _currentDirection = Directions.Top;
            }else if (_direction is { x: < 0.0f, y: < 0.0f })
            {
                _currentDirection = Directions.BottomLeft;
            }else if (_direction is { x: > 0.0f, y: < 0.0f })
            {
                _currentDirection = Directions.BottomRight;
            }else if (_direction is { x: < 0.0f, y: > 0.0f })
            {
                _currentDirection = Directions.TopLeft;
            }else if (_direction is { x: > 0.0f, y: > 0.0f })
            {
                _currentDirection = Directions.TopRight;
            }
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
        ChangeHandItem(context, 0);
    }
    
    public void OnItem2Press(InputAction.CallbackContext context)
    {
        ChangeHandItem(context, 1);
    }
    
    public void OnItem3Press(InputAction.CallbackContext context)
    {
        ChangeHandItem(context, 2);
    }

    private void ChangeHandItem(InputAction.CallbackContext context, int index)
    {
        if (!context.performed) return;
        Debug.Log(index + 1);
        _activeHandItem = index;

        if (_isPlacing)
        {
            Destroy(_placingObject);
            _isPlacing = false;
        }

        if (!_playerInventory.HasIndex(index))
        {
            _activeHandItem = index;
        }
    }
    
    public void OnPlaceItem(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (_playerInventory.CanPlaceIndex(_activeHandItem))
        {
            _placingObject = Instantiate(_playerInventory.GetPlaceIndex(_activeHandItem));
            _isPlacing = true;
        }
    }

    public void OnDropItem(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        _playerInventory.DropItem(_activeHandItem, GetDirectionVector2());
    }

    private Vector2 GetDirectionVector2()
    {
        switch (_currentDirection)
        {
            case Directions.Top:
                return Vector2.up;
            case Directions.Bottom:
                return Vector2.down;
            case Directions.Left:
                return Vector2.left;
            case Directions.Right:
                return Vector2.right;
            case Directions.TopLeft:
                return (Vector2.up + Vector2.left).normalized;
            case Directions.TopRight:
                return (Vector2.up + Vector2.right).normalized;
            case Directions.BottomLeft:
                return (Vector2.down + Vector2.left).normalized;
            case Directions.BottomRight:
                return (Vector2.down + Vector2.right).normalized;
            default:
                return Vector2.zero;
        }
    }
}
