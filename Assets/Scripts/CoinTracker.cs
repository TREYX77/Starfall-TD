using TMPro;
using UnityEngine;

public class CoinTracker : MonoBehaviour
{
    public static CoinTracker Instance; // Singleton zodat andere scripts makkelijk coins kunnen toevoegen

    public TextMeshProUGUI coinText;
    private int coins = 50; // Begin met 50 coins

    void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        if (coinText == null)
            coinText = GetComponent<TextMeshProUGUI>();

        UpdateCoinDisplay();
    }

    public bool CanSpend(int amount)
    {
        return coins >= amount;
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            UpdateCoinDisplay();
            return true;
        }
        return false;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinDisplay();
    }

    private void UpdateCoinDisplay()
    {
        if (coinText != null)
            coinText.text = "Coins: " + coins;
    }

    // Call this method wanneer een enemy dood gaat
    public void OnEnemyDestroyed(GameObject enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            AddCoins(5); // Geef 5 coins per enemy
        }
    }
}
