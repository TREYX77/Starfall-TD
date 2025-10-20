using TMPro;
using UnityEngine;

public class CoinTracker : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private int coins = 0;

    void Start()
    {
        if (coinText == null)
            coinText = GetComponent<TextMeshProUGUI>();

        UpdateCoinDisplay();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinDisplay();
    }

    private void UpdateCoinDisplay()
    {
        coinText.text = "Coins: " + coins;
    }

    // Call this method when an enemy is destroyed
    public void OnEnemyDestroyed(GameObject enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            AddCoins(5);
        }
    }
}