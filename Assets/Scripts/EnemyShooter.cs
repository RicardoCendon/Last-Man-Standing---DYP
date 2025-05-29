using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float fireRate = 2f; // Tiempo entre disparos

    private float fireTimer;
    public Transform target; // Asigna el jugador manualmente desde el inspector


    void Update()
    {
        if (target == null) return;

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            fireTimer = 0f;

            // Apuntar al jugador
            Vector3 direction = (target.position - firePoint.position).normalized;
            firePoint.rotation = Quaternion.LookRotation(direction); // Opcional: gira el firePoint hacia el jugador

            // Instanciar y lanzar la bala
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * bulletSpeed;

        }
    }
}






