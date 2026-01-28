using UnityEngine;

public class Currency : MonoBehaviour
{
    public int Coins { get; private set; } = 0;

    public void AddCoins(int coins)
    {
        Coins += coins;
    }

}
