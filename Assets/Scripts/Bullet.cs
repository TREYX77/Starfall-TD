using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public float lifetime = 3f;

    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Richt naar target
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        // Draai bullet in de richting van de target (optioneel)
        transform.forward = dir;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            // Hier damage toepassen
        }
    }
}
