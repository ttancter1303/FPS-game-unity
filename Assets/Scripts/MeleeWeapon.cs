using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] MeleeWeaponSO weaponSO;
    [SerializeField] Transform attackOrigin;
    [SerializeField] Animator animator;

    float lastAttackTime;

    const string ATTACK_ANIM = "MeleeAttack";

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time - lastAttackTime >= weaponSO.AttackRate)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        // Play animation
        if (animator != null)
            animator.Play(ATTACK_ANIM, 0, 0f);

        // Damage logic
        RaycastHit hit;
        if (Physics.Raycast(attackOrigin.position, attackOrigin.forward, out hit, weaponSO.AttackRange, weaponSO.HitLayers))
        {
            // Gây sát thương
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage);

            // Tác động lực vật lý nếu có Rigidbody
            Rigidbody rb = hit.collider.attachedRigidbody;
            if (rb != null)
            {
                Vector3 forceDirection = attackOrigin.forward; // dùng hướng chém
                rb.AddForce(forceDirection.normalized * weaponSO.WeaponForce, ForceMode.Impulse);
            }

            // Tác động lên các vật thể phát nổ
            var explodeTarget = hit.collider.GetComponent<ExplosionOnHit>();
            explodeTarget?.Explode();
        }
    }
}
