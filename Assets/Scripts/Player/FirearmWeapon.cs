using UnityEngine;
using Cinemachine;

public class FirearmWeapon : Weapon
{
    [SerializeField] private FirearmWeaponSO weaponSO; // Tham chiếu đến dữ liệu vũ khí tầm xa (FirearmWeaponSO)
    [SerializeField] Transform bulletSpawn;
    [SerializeField] ParticleSystem muzzleFlash;


    Animator animator;
    const string SHOOT_STRING = "Shoot";
    CinemachineImpulseSource impulseSource;
    AudioSource audioSource;

    public float recoilAmount = 2f;
    public float recoilSpeed = 5f;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }
    private void Start()
    {
        animator = GetComponent<Animator>();

    }
    public override void Attack()
    {

        animator.ResetTrigger(SHOOT_STRING);
        animator.SetTrigger(SHOOT_STRING);
        impulseSource.GenerateImpulse();
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionsLayer, QueryTriggerInteraction.Ignore))
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

            audioSource.Play();
            // Gây sát thương
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage);

            // Tác động lực vật lý nếu có Rigidbody
            Rigidbody rb = hit.collider.attachedRigidbody;
            if (rb != null)
            {
                Vector3 forceDirection = hit.point - bulletSpawn.position;
                rb.AddForceAtPosition(forceDirection.normalized * weaponSO.WeaponForce, hit.point, ForceMode.Impulse);
            }
            // Tác động lên các vật thể phát nổ
            var explodeTarget = hit.collider.GetComponent<ExplosiveOnHit>();
            explodeTarget?.TakeDamage(weaponSO.Damage);
            ApplyRecoil();

        }
        void ApplyRecoil()
        {
            // Giật camera lên
            float recoilX = Random.Range(recoilAmount * 0.5f, recoilAmount);
            float recoilY = Random.Range(-recoilAmount * 0.2f, recoilAmount * 0.2f);

            Camera.main.transform.localEulerAngles += new Vector3(-recoilX, recoilY, 0f);
        }
    }
}
