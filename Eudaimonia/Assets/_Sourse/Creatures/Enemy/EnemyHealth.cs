using UnityEngine;

public class EnemyHealth : AHealth
{
    protected override void Die()
    {
        gameObject.SetActive(false);
    }
}
