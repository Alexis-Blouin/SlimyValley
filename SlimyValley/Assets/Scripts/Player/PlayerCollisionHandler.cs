using System;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Item"))
        {
            Destroy(other.gameObject);
        }
    }
}
