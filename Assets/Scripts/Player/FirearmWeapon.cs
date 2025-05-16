using UnityEngine;
using Cinemachine;

public class FirearmWeapon : Weapon
{
    [SerializeField] private FirearmWeaponSO weaponSO; // Dữ liệu vũ khí (damage, force, prefab VFX, ...)
    [SerializeField] Transform bulletSpawn;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] float recoilAngle = 1f;

    Animator animator;
    const string SHOOT_STRING = "Shoot";
    CinemachineImpulseSource impulseSource;
    AudioSource audioSource;
    HurtBox hurtBox;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        hurtBox = GetComponent<HurtBox>();
    }

    public override void Attack()
    {
        // Reset và set trigger animation bắn
        animator.ResetTrigger(SHOOT_STRING);
        animator.SetTrigger(SHOOT_STRING);

        // Tạo impulse cho camera thông qua Cinemachine
        impulseSource.GenerateImpulse();

        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // Tính hướng bắn có recoil
        Vector3 shootDirection = GetRandomizedDirection(Camera.main.transform.forward, recoilAngle);

        // Thực hiện raycast từ camera chính với hướng bắn đã lệch
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, shootDirection, out hit,
            Mathf.Infinity, interactionsLayer, QueryTriggerInteraction.Ignore))
        {
            // Spawn hit VFX tại điểm trúng
            Vector3 directionToPlayer = Camera.main.transform.position - hit.point;
            Quaternion rotation = Quaternion.LookRotation(directionToPlayer.normalized);


            GameObject hitVFX = Instantiate(weaponSO.HitVFXPrefab, hit.point, rotation);



            ParticleSystem ps = hitVFX.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Destroy(hitVFX, ps.main.duration + ps.main.startLifetime.constantMax);
            }
            else
            {
                Destroy(hitVFX, 2f); // fallback nếu không có particle system
            }

            // Phát âm thanh
            audioSource.Play();

            // Gây sát thương cho enemy nếu có
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage);

            if (hurtBox != null)
            {
                hurtBox.TakeDamage(weaponSO.Damage);
            }

            // Nếu đối tượng có Rigidbody, tác động lực vật lý
            Rigidbody rb = hit.collider.attachedRigidbody;
            if (rb != null)
            {
                Vector3 forceDirection = hit.point - bulletSpawn.position;
                rb.AddForceAtPosition(forceDirection.normalized * weaponSO.WeaponForce, hit.point, ForceMode.Impulse);
            }

            // Kiểm tra các đối tượng có hiệu ứng nổ
            var explodeTarget = hit.collider.GetComponent<ExplosiveOnHit>();
            explodeTarget?.TakeDamage(weaponSO.Damage);
        }
    }

    Vector3 GetRandomizedDirection(Vector3 forward, float angle)
    {
        return Quaternion.Euler(
            Random.Range(-angle, angle),
            Random.Range(-angle, angle),
            0f
        ) * forward;
    }
}
