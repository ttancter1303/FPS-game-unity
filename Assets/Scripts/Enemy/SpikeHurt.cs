using UnityEngine;

public class SpikeHurt : MonoBehaviour
{
    public int damageAmount = 40;
    public float damageInterval = 1f;

    private float lastDamageTime;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time - lastDamageTime >= damageInterval)
            {
                PlayerHealth health = other.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(damageAmount);
                    lastDamageTime = Time.time;
                }
            }
        }
    }
}