using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Slime : MonoBehaviour
{
    [SerializeField] private float wanderRadius = 5.0f;
    [SerializeField] private float idleTime = 2.0f;
    
    private NavMeshAgent _agent;
    private bool _waiting;
    
    private Animator _animator;
    private int _idleCount = 3;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        
        PickNextIdle();
    }

    private void Update()
    {
        // _agent.SetDestination(target.position);
        if (_waiting) return;

        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            StartCoroutine(WaitAndPickNew());
        }
    }

    public void PickNextIdle()
    {
        _animator.SetInteger("IdleIndex", Random.Range(0, _idleCount));
    }

    private void PickNewDestination()
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * wanderRadius;

        if (NavMesh.SamplePosition(
                randomPoint,
                out NavMeshHit hit,
                wanderRadius,
                _agent.areaMask))
        {
            _agent.SetDestination(hit.position);
        }
    }

    private IEnumerator WaitAndPickNew()
    {
        _waiting = true;
        yield return new WaitForSeconds(idleTime);
        _waiting = false;
        PickNewDestination();
    }
}
