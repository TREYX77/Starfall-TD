using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Enemy enemy;
    public Slider healthSlider;
    public Transform target;
    public Vector3 offset = new Vector3(0, 1f, 0);

    void Start()
    {
        if (!enemy)
            enemy = GetComponentInParent<Enemy>();

        if (healthSlider)
        {
            healthSlider.maxValue = 100f; // vaste max
            healthSlider.value = enemy.health;
        }
    }

    void Update()
    {
        if (!enemy || !healthSlider) return;

        healthSlider.value = enemy.health;

        if (target)
            healthSlider.transform.position = target.position + offset;

        healthSlider.transform.LookAt(Camera.main.transform);
        healthSlider.transform.Rotate(0, 180, 0);
    }
}
