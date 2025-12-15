using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject attackNode;
    [SerializeField] private Vector2 attackOffset;

    private Vector2 _direction;
    
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private AttackArea _attackBoxCollider;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _attackBoxCollider = attackNode.GetComponent<AttackArea>();
    }

    void Update()
    {
        transform.Translate(_direction * (speed * Time.deltaTime), Space.World);
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

    public void OnAttack(InputAction.CallbackContext context)
    {
        _attackBoxCollider.DoAttack();
    }
}
