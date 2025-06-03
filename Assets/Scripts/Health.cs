using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;


public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public float damageMultiplier = 1f;

    [Header("Solo para el jugador")]
    public TextMeshProUGUI healthText;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        int finalDamage = Mathf.RoundToInt(damage * damageMultiplier);
        currentHealth -= finalDamage;

        GetComponent<FlashOnHit>()?.Flash();
        UpdateUI();

        Debug.Log($"{gameObject.name} took {finalDamage} damage. Remaining Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            if (CompareTag("Player"))
            {
                Destroy(gameObject);
                Debug.Log("Player has died.");
                SceneManager.LoadScene("Menú");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }


    private void UpdateUI()
    {
        if (healthText != null)
        {
            healthText.text = "HEALTH: " + currentHealth;
        }
    }

}



