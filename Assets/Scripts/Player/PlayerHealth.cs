using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 100;


    int currentHealth;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Take damage: "+amount);  

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
