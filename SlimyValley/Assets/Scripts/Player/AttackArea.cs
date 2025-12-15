using System;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private int damage;
    
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
        if (other.CompareTag("Attackable"))
        {
            Debug.Log("Hit " + other.name);
            Tree tree = other.gameObject.GetComponent<Tree>();
            tree.GetHit(damage);
        }
    }
}
