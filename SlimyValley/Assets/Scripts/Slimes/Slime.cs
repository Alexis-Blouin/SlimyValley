using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Slime : MonoBehaviour
{
    private Animator _animator;
    private int _idleCount = 3;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        PickNextIdle();
    }

    public void PickNextIdle()
    {
        _animator.SetInteger("IdleIndex", Random.Range(0, _idleCount));
    }
}
