using Cinemachine;
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 10;
    [SerializeField] ParticleSystem particle;
    Animator animator;
    BaseEnemy baseEnemy;


    float currentHealth;

    void Awake()
    {
        currentHealth = startingHealth;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        baseEnemy = GetComponent<BaseEnemy>();

    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        //Instantiate(particle, transform.position, Quaternion.identity);
        if (currentHealth <= 0)
        {
            Die();
            
        }
    }

    public void Die()
    {
        animator.SetTrigger("Die");

        if (baseEnemy)
        {
            if (baseEnemy.agent) baseEnemy.agent.enabled = false;
            baseEnemy.enabled = false; // Ngừng update() trong BaseEnemy
        }

        // Tắt toàn bộ script khác, trừ EnemyHealth
        MonoBehaviour[] allScripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in allScripts)
        {
            if (script != this) script.enabled = false;
        }
        DestroyWithDelay();
    }
    void DestroyWithDelay()
    { 
        Destroy(gameObject,4f);

    }

}
