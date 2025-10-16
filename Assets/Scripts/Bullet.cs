using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 20f;
    public float damage = 25f;

    public enum BulletType { Normal, Freeze, Fire }
    public BulletType bulletType;

    public float freezeDuration = 2f;
    public float freezeSlowAmount = 0.5f; // 50% slower
    public float burnDuration = 5f;
    public float burnTickDamage = 10f;
    public float burnTickInterval = 1f;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        Enemy e = target.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage);

            switch (bulletType)
            {
                case BulletType.Freeze:
                    e.ApplyFreeze(freezeSlowAmount, freezeDuration);
                    break;
                case BulletType.Fire:
                    e.ApplyBurn(burnTickDamage, burnDuration, burnTickInterval);
                    break;
            }
        }

        Destroy(gameObject);
    }
}
