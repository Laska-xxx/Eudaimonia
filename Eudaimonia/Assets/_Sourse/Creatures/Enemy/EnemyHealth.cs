using UnityEngine;

public class EnemyHealth : AHealth
{
    [SerializeField] private int reward = 5;
    private Currency _currency;

    public void Init(Currency currency)
    {
        _currency = currency;
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
        _currency.AddCoins(reward);
    }
}
