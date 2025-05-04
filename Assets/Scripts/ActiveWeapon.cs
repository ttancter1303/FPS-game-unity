using StarterAssets;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;


    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    Weapon currentWeapon;

    const string SHOOT_STRING = "Shoot";
    float timeCooldow = 0f;

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
    }
    private void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        timeCooldow += Time.deltaTime;
        HandleShoot();
    }

    void HandleShoot()
    {
        if (!starterAssetsInputs.shoot) return;


        // Play effect + animation
        if (timeCooldow >= weaponSO.FireRate)
        {
            currentWeapon.Shoot(weaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            timeCooldow = 0f;
        }
        if (!weaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false); // reset input

        }
    }
    public void SwitchWeapon(WeaponSO weaponSO)
    {
        Debug.Log("Player pick up: "+ weaponSO.name);

    }
}
