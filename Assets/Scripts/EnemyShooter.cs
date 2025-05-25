using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootInterval = 2f;
    public float bulletSpeed = 12f;
    public float detectionRange = 15f;

    private Transform target;
    private float shootTimer;

    void Start()
    {
        target = GameObject.FindWithTag("Player")?.transform;
    }

    void Update()
    {
        if (target == null || firePoint == null || bulletPrefab == null) return;

        float dist = Vector3.Distance(transform.position, target.position);
        if (dist <= detectionRange)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                Shoot();
                shootTimer = 0f;
            }

            // Girar hacia el jugador
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0;
            transform.forward = direction;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }
    }
}


