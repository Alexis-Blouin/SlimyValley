using UnityEngine;

public class Log : MonoBehaviour
{
    private Vector2 _startPos;
    private Vector2 _targetPos;
    private float _moveTime = 0.3f;
    private float _timer;

    void Start()
    {
        _startPos = transform.position;

        Vector2 randomDir = Random.insideUnitCircle.normalized;
        float distance = Random.Range(0.5f, 1.5f);

        _targetPos = _startPos + randomDir * distance;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        float t = _timer / _moveTime;

        transform.position = Vector2.Lerp(_startPos, _targetPos, t);
    }
}
