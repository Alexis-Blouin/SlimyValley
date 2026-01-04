using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private int hp;

    public void GetHit(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}
