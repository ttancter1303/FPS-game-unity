using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 10;
    [SerializeField] ParticleSystem particle;

    int currentHealth;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            SelfDestruct();
            
        }
    }
    public void SelfDestruct()
    {
        Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
