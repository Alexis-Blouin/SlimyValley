using UnityEngine;

public class InstanceItemContainer : MonoBehaviour
{
    public ItemInstance item;
    
    private Vector2 _startPos;
    private Vector2 _targetPos;
    private float _moveTime = 0.3f;
    private float _timer;

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

    void Update()
    {
        _timer += Time.deltaTime;
        var t = _timer / _moveTime;

        transform.position = Vector2.Lerp(_startPos, _targetPos, t);
    }
}
