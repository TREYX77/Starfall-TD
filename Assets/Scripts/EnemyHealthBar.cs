using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Enemy enemy;             // Link naar de enemy
    public Slider healthSlider;     // De slider UI
    public Transform target;        // Waar de slider boven moet hangen
    public Vector3 offset = new Vector3(0, 1f, 0); // Hoogte boven de enemy

    void Start()
    {
        if (!enemy)
            enemy = GetComponentInParent<Enemy>();

        if (!target && enemy)
            target = enemy.transform;

        if (healthSlider && enemy)
        {
            healthSlider.maxValue = enemy.maxHealth; // dynamisch max
            healthSlider.value = enemy.health;
        }
    }

    void Update()
    {
        if (!enemy || !healthSlider) return;

        // Update slider waarde
        healthSlider.value = enemy.health;

        // Zet slider boven de enemy
        if (target)
            healthSlider.transform.position = target.position + offset;

        // Laat slider naar de camera kijken
        healthSlider.transform.LookAt(Camera.main.transform);
        healthSlider.transform.Rotate(0, 180, 0);

        // Optioneel: verberg slider als enemy dood is
        if (enemy.health <= 0)
        {
            healthSlider.gameObject.SetActive(false);
        }
    }
}
