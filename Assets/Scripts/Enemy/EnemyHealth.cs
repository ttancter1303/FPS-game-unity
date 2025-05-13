using Cinemachine;
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
        Instantiate(particle, transform.position, Quaternion.identity);
        if (currentHealth <= 0)
        {
            SelfDestruct();

            
        }
    }
    public void SelfDestruct()
    {
   
        Destroy(this.gameObject);
    }
}
