using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class EnemyDamageEntry
{
    public GameObject enemyPrefab;
    public int damage;
}

public class BaseHp : MonoBehaviour
{
    
    [SerializeField] private int maxHp = 100;
    [SerializeField] private int currentHp;

    
    [SerializeField] private Slider healthBar;

    
    [SerializeField] private List<EnemyDamageEntry> enemyDamageList = new();

    void Start()
    {
        currentHp = maxHp;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHp;
            healthBar.value = currentHp;
        }
    }

   
    public void TakeDamage(int damage)
    {
        currentHp = Mathf.Max(currentHp - damage, 0);
        if (healthBar != null)
        {
            healthBar.value = currentHp;
        }
       
    }

   
    public void SetHealthBarColor(Color color)
    {
        if (healthBar != null && healthBar.fillRect != null)
        {
            healthBar.fillRect.GetComponent<Image>().color = color;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int damage = GetDamageForEnemy(collision.gameObject);
            TakeDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            int damage = GetDamageForEnemy(other.gameObject);
            TakeDamage(damage);
        }
    }

    private int GetDamageForEnemy(GameObject Enemy)
    {
        foreach (var entry in enemyDamageList)
        {
            // Compare prefab reference or use a component for identification
            if (Enemy.name.Contains(entry.enemyPrefab.name))
            {
                return entry.damage;
            }
        }
        // Default damage if not found
        return 10;
    }
}
