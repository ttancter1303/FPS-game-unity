using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO startingWeaponSO;
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] GameObject zoomVignette;
    [SerializeField] GameObject crosshair;
    [SerializeField] TMP_Text ammoText;

    WeaponSO currentWeaponSO;
    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    Weapon currentWeapon;
    FirstPersonController firstPersonController;

    const string SHOOT_STRING = "Shoot";
    float timeCooldow = 0f;
    float defaultFOV;
    float defaultRotationSpeed;
    int currentAmmo;
    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
    }
    private void Start()
    {
        SwitchWeapon(startingWeaponSO);
        firstPersonController = GetComponentInParent<FirstPersonController>();

        animator = GetComponent<Animator>();
        defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }

    void Update()
    {
        timeCooldow += Time.deltaTime;
        HandleShoot();
        HandleZoom();
    }
    void AdjustAmmo(int amount)
    {
        currentAmmo += amount;
        ammoText.text = currentAmmo.ToString("D2");
    }
    void HandleShoot()
    {
        if (!starterAssetsInputs.shoot) return;


        // Play effect + animation
        if (timeCooldow >= currentWeaponSO.FireRate)
        {
            currentWeapon.Shoot(currentWeaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            timeCooldow = 0f;
        }
        if (!currentWeaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false); // reset input

        }
    }
    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (weaponSO)
        {
            Destroy(currentWeapon.gameObject);
        }
        Weapon newWeapon = Instantiate(weaponSO.WeaponPrefab, transform).GetComponent<Weapon>();
        currentWeapon = newWeapon;
        this.currentWeaponSO = weaponSO;
        if(!weaponSO.CrosshairOff)
        {
            crosshair.SetActive(true);
        }
        else
        {
            crosshair.SetActive(false);
        }

    }
    public void HandleZoom()
    {
        if(!currentWeaponSO.CanZoom) return;
        
        if (starterAssetsInputs.zoom)
        {
            playerFollowCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
            zoomVignette.SetActive(true);
            crosshair.SetActive(true);
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.RotationSpeed);

        }
        else
        {
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
            crosshair.SetActive(false);
            playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
            zoomVignette.SetActive(false);
        }
    }
}
