using UnityEngine;

public abstract class AHealth : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    [SerializeField] private HealthView healthView;

    private int _curHealth;

    private void Awake()
    {
        _curHealth = maxHealth;
        healthView.Init(maxHealth);
    }

    public void GetDamage(int damage)
    {
        _curHealth -= damage;
        healthView.UpdateHealth(damage);

        if (_curHealth <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}
