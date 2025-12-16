using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class InstanceItemContainer : MonoBehaviour
{
    public ItemInstance item;
    
    private Vector2 _startPos;
    private Vector2 _targetPos;
    private float _moveTime = 0.3f;
    private float _timer;
    
    private bool _canTake = false;
    private float _canTakeTimer = 0.5f;

    private void Start()
    {
        StartCoroutine(EnableCanTakeAfterDelay());
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        var t = _timer / _moveTime;

        transform.position = Vector2.Lerp(_startPos, _targetPos, t);
    }

    public bool CanTakeItem()
    {
        return _canTake;
    }

    public ItemInstance TakeItem()
    {
        Destroy(gameObject);
        return item;
    }

    public void MoveOndrop(Vector2 ?direction = null)
    {
        _startPos = transform.position;

        var randomDir = direction ?? Random.insideUnitCircle.normalized;
        var distance = Random.Range(0.5f, 1.5f);

        _targetPos = _startPos + randomDir * distance;
    }
    
    private IEnumerator EnableCanTakeAfterDelay()
    {
        yield return new WaitForSeconds(_canTakeTimer);
        _canTake = true;
    }
}
