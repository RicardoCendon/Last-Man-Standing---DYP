using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 1;
    public float lifeTime = 2f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡El jugador fue alcanzado!");
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }

        else if (!other.CompareTag("Enemy"))
        {
            // Destruir si choca con algo que no sea enemigo (pared, suelo, etc.)
            Destroy(gameObject);
        }
    }
}
