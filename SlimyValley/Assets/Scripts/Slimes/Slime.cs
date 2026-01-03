using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Slime : MonoBehaviour
{
    [SerializeField] private float wanderRadius = 5.0f;
    [SerializeField] private float idleTime = 2.0f;
    
    [SerializeField] private float visionRange = 3.0f;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;
    
    private NavMeshAgent _agent;
    private bool _waiting;
    
    private Transform _target;
    
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private int _idleCount = 3;
    
    private bool CanSeeTarget => _target != null;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (CanSeeTarget)
        {
            Debug.Log(_target.position);
            _agent.SetDestination(_target.position);
        }
        else
        {
            if (_waiting) return;

            if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                StartCoroutine(WaitAndPickNew());
            }
        }

        var movement = _agent.velocity;
        _animator.SetBool("Xmov", movement.x != 0.0f);
        // _animator.SetFloat("Ymov", movement.y);

        if (movement.x != 0.0f)
        {
            _spriteRenderer.flipX = movement.x < 0.0f;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & targetMask) != 0)
        {
            if (HasLineOfSight(other.transform))
            {
                _target = other.transform;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.transform == _target)
            _target = null;
    }

    private bool HasLineOfSight(Transform target)
    {
        Vector2 dir = (target.position - transform.position).normalized;
        float dist = Vector2.Distance(transform.position, target.position);
        
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            dir,
            dist,
            obstacleMask);
        
        return hit.collider == null;
    }
}
