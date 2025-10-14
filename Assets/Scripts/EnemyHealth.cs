using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float progress = 0f; // Hoe ver hij is op het pad

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
