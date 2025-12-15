using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector2 _direction;
    
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * (speed * Time.deltaTime), Space.World);
    }
}
