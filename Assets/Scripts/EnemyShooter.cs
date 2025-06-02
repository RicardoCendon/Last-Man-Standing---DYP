using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("Disparo")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    [Range(0.1f, 7f)]
    public float fireRate = 5f; // Tiempo entre disparos
    public float bulletSpeed = 20f;

    [Header("Target")]

    public Transform target; // El jugador

    private float fireTimer = 0f;

    void Start()
    {
        // Si no fue asignado manualmente, busca al jugador por tag
        if (target == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
                target = playerObject.transform;
        }
    }

    void Update()
    {
        if (target == null) return;

        // Temporizador para controlar frecuencia de disparo
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            fireTimer = 0f;

            // Apuntar hacia el jugador
            Vector3 direction = (target.position - firePoint.position).normalized;
            firePoint.rotation = Quaternion.LookRotation(direction);

            // Instanciar y lanzar la bala
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }
    }
}






