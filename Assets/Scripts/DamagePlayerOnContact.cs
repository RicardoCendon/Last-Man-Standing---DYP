using UnityEngine;

public class DamagePlayerOnContact : MonoBehaviour
{
    public int damageAmount = 10;
    public float damageCooldown = 1f; // tiempo entre cada daño
    private float nextDamageTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Time.time >= nextDamageTime)
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                nextDamageTime = Time.time + damageCooldown;
            }
        }
    }
}

