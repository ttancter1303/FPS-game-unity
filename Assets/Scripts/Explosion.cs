using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;
    [SerializeField] int damage = 30;
    const string PLAYER_STRING = "Player";
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Explode();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hitCollider in hitColliders)
        {
            EnemyHealth enemyHealth = hitCollider.GetComponent<EnemyHealth>();
            if (enemyHealth)
            {
                audioSource.Play();

                enemyHealth.TakeDamage(damage);
                break;
            }
            var explosionObj = hitCollider.GetComponent<ExplosiveOnHit>();
            if (explosionObj)
            {
                audioSource.Play();

                explosionObj.TakeDamage(damage);
                break;
            }

            PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                audioSource.Play();

                playerHealth.TakeDamage(damage);
                break;
            }
        }
    }

}
