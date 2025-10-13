using UnityEngine;

public class TowerShoot : MonoBehaviour
{
    [Header("Tower Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float range = 8f;
    public float turnSpeed = 5f;

    private float fireTimer;
    private Transform target;

    void Update()
    {
        FindTarget();
        RotateToTarget();

        if (target != null)
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0f)
            {
                Shoot();
                fireTimer = 1f / fireRate;
            }
        }
    }

    void FindTarget()
    {
        // Zoek vijanden op basis van tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDist = Mathf.Infinity;
        Transform firstEnemy = null;

        foreach (GameObject e in enemies)
        {
            float dist = Vector3.Distance(transform.position, e.transform.position);
            if (dist <= range && dist < closestDist)
            {
                closestDist = dist;
                firstEnemy = e.transform;
            }
        }

        target = firstEnemy;
    }

    void RotateToTarget()
    {
        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        dir.y = 0f; // alleen horizontaal draaien
        Quaternion lookRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * turnSpeed);
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null || target == null) return;

        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.SetTarget(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
