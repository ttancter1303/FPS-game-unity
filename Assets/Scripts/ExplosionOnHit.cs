using UnityEngine;

public class ExplosiveOnHit : MonoBehaviour
{

    [SerializeField] int startingHealth = 1;
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
