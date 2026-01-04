using UnityEngine;

public class SlimeHealth : Health
{
    protected override void Die()
    {
        Debug.Log("Slime dead");
    }
}
