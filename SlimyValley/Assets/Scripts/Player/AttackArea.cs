using System;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _collider.enabled = false;
    }

    public void DoAttack()
    {
        _collider.enabled = true;
        Invoke(nameof(DisableCollider), 0.1f); // attack window
    }

    private void DisableCollider()
    {
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit " + other.name);
        // Apply damage here
    }
}
