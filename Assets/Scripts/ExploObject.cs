using UnityEngine;

public class ExploObject : MonoBehaviour
{
    [SerializeField] GameObject explosionVFXPrefab;

    public void Explode()
    {
        if (explosionVFXPrefab != null)
        {
            GameObject vfx = Instantiate(explosionVFXPrefab, transform.position, Quaternion.identity);
            ParticleSystem ps = vfx.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Destroy(vfx, ps.main.duration + ps.main.startLifetime.constantMax);
            }
            else
            {
                Destroy(vfx, 2f); // fallback
            }
        }

        Destroy(gameObject); // xoá object sau khi nổ (tuỳ mục đích)
    }

}
