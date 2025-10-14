using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TowerShoot : MonoBehaviour
{
    [Header("Targeting")]
    public string enemyTag = "Enemy"; // selecteerbaar in Inspector
    public float range = 10f;
    private Transform target;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float bulletDamage = 20f;

    [Header("Checkpoint")]
    public Transform checkpoint; // wordt automatisch gevuld via tag

    void Start()
    {
        // automatische checkpoint vinden als er nog geen is toegewezen
        if (checkpoint == null)
        {
            GameObject cp = GameObject.FindGameObjectWithTag("Checkpoint");
            if (cp != null)
                checkpoint = cp.transform;
            else
                Debug.LogWarning("Geen checkpoint gevonden in de scene! Voeg een GameObject met tag 'Checkpoint' toe.");
        }

        // update target 2x per seconde
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        if (enemies.Length == 0)
        {
            target = null;
            return;
        }

        GameObject firstEnemy = null;
        float maxProgress = float.MinValue;

        foreach (GameObject enemyGO in enemies)
        {
            float distanceToTower = Vector3.Distance(transform.position, enemyGO.transform.position);
            if (distanceToTower <= range)
            {
                Enemy e = enemyGO.GetComponent<Enemy>();
                if (e != null && e.progress > maxProgress)
                {
                    maxProgress = e.progress;
                    firstEnemy = enemyGO;
                }
            }
        }

        // pak de Transform van het GameObject
        target = (firstEnemy != null) ? firstEnemy.transform : null;
    }

    void Update()
    {
        if (target == null) return;

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null || target == null) return;

        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.damage = bulletDamage;
            bullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(TowerShoot))]
public class TowerShootEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TowerShoot tower = (TowerShoot)target;

        // Dropdown voor enemy tag
        tower.enemyTag = EditorGUILayout.TagField("Enemy Tag", tower.enemyTag);

        DrawDefaultInspector();
    }
}
#endif
