using StarterAssets;
using Unity.Mathematics;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] Transform bulletSpawn;
    public void Shoot(WeaponSO weaponSO)
    {

        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            // Spawn effect and auto-destroy
            GameObject hitVFX = Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);
            ParticleSystem ps = hitVFX.GetComponent<ParticleSystem>();

            if (ps != null)
            {
                Destroy(hitVFX, ps.main.duration + ps.main.startLifetime.constantMax);
            }
            else
            {
                Destroy(hitVFX, 2f); // fallback nếu không có ParticleSystem
            }

            // Gây sát thương
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage);

            // Tác động lực vật lý nếu có Rigidbody
            Rigidbody rb = hit.collider.attachedRigidbody;
            if (rb != null)
            {
                Vector3 forceDirection = hit.point - bulletSpawn.position;
                rb.AddForceAtPosition(forceDirection.normalized * 20f, hit.point, ForceMode.Impulse);
            }
            // Tác động lên các vật thể phát nổ
            var explodeTarget = hit.collider.GetComponent<ExplosionOnHit>();
            explodeTarget?.Explode();


        }
    }

}
