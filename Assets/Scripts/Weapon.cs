using StarterAssets;
using Unity.Mathematics;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject hitVFXPrefab;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] int damageAmount = 1;

    StarterAssetsInputs starterAssetsInputs;

    const string SHOOT_STRING = "Shoot";

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
    }

    void Update()
    {
        HandleShoot();
    }

    void HandleShoot()
    {
        if (!starterAssetsInputs.shoot) return;

        // Play effect + animation
        muzzleFlash.Play();
        animator.Play(SHOOT_STRING, 0, 0f);
        starterAssetsInputs.ShootInput(false); // reset input

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            // Spawn effect and auto-destroy
            GameObject hitVFX = Instantiate(hitVFXPrefab, hit.point, Quaternion.identity);
            ParticleSystem ps = hitVFX.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Destroy(hitVFX, ps.main.duration + ps.main.startLifetime.constantMax);
            }
            else
            {
                Destroy(hitVFX, 2f); // fallback nếu không có ParticleSystem
            }

            // Apply damage
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(damageAmount);
            var explodeTarget = hit.collider.GetComponent<ExploObject>();
            explodeTarget?.Explode();


        }
    }

}
