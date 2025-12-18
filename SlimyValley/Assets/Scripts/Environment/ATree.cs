using UnityEngine;

public class ATree : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] private int minLog;
    [SerializeField] private int maxLog;
    [SerializeField] private GameObject log;

    public void GetHit(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        for (var i = Random.Range(minLog, maxLog); i <= maxLog; i++)
        {
            var droppedLog = Instantiate(log, transform.position, Quaternion.identity);
            droppedLog.GetComponent<InstanceItemContainer>().MoveOndrop();
        }
        Destroy(gameObject);
    }
}
