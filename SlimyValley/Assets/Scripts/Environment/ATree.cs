using UnityEngine;

public class ATree : Health
{
    [SerializeField] private int minLog;
    [SerializeField] private int maxLog;
    [SerializeField] private GameObject log;

    protected override void Die()
    {
        for (var i = Random.Range(minLog, maxLog); i <= maxLog; i++)
        {
            var droppedLog = Instantiate(log, transform.position, Quaternion.identity);
            droppedLog.GetComponent<InstanceItemContainer>().MoveOndrop();
        }
        Destroy(gameObject);
    }
}
