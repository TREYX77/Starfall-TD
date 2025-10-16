using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float progress = 0f; // Hoe ver hij is op het pad
    public float speed = 10f;

    private float originalSpeed;

    void Start()
    {
        originalSpeed = speed;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    public void ApplyFreeze(float slowAmount, float duration)
    {
        StopCoroutine(nameof(FreezeEffect));
        StartCoroutine(FreezeEffect(slowAmount, duration));
    }

    private IEnumerator FreezeEffect(float slowAmount, float duration)
    {
        speed *= slowAmount;
        yield return new WaitForSeconds(duration);
        speed = originalSpeed;
    }

    public void ApplyBurn(float tickDamage, float duration, float interval)
    {
        StopCoroutine(nameof(BurnEffect));
        StartCoroutine(BurnEffect(tickDamage, duration, interval));
    }

    private IEnumerator BurnEffect(float tickDamage, float duration, float interval)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            TakeDamage(tickDamage);
            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
