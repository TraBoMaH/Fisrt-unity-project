using UnityEngine;
public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100; // Максимальное здоровье врага
    public float currentHealth; // Текущее здоровье врага 
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}