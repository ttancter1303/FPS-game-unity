using UnityEngine;

public class ExplosionOnHit : MonoBehaviour
{
    [Header("Explosion Settings")]
    [SerializeField] GameObject explosionVFXPrefab;
    [SerializeField] float explosionForce = 500f;
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] float upwardModifier = 0.5f;
    [SerializeField] LayerMask affectedLayers;

    [Header("Self Destruct")]
    [SerializeField] bool destroySelf = true;

    public void Explode()
    {
        // Spawn VFX
        if (explosionVFXPrefab != null)
        {
            GameObject vfx = Instantiate(explosionVFXPrefab, transform.position, Quaternion.identity);
            ParticleSystem ps = vfx.GetComponent<ParticleSystem>();
            if (ps != null)
                Destroy(vfx, ps.main.duration + ps.main.startLifetime.constantMax);
            else
                Destroy(vfx, 2f);
        }

        // Apply force to nearby rigidbodies
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, affectedLayers);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.attachedRigidbody;
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardModifier, ForceMode.Impulse);
            }
        }

        // Destroy this object
        if (destroySelf)
        {
            Destroy(gameObject);
        }
    }
}
