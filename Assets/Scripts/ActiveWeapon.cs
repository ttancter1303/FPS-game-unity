using Cinemachine;
using StarterAssets;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] GameObject zoomVignette;
    [SerializeField] GameObject crosshair;

    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    Weapon currentWeapon;
    FirstPersonController firstPersonController;

    const string SHOOT_STRING = "Shoot";
    float timeCooldow = 0f;
    float defaultFOV;
    float defaultRotationSpeed;
    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
    }
    private void Start()
    {
        firstPersonController = GetComponentInParent<FirstPersonController>();
        currentWeapon = GetComponentInChildren<Weapon>();
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
        if (weaponSO)
        {
            Destroy(currentWeapon.gameObject);
        }
        Weapon newWeapon = Instantiate(weaponSO.WeaponPrefab, transform).GetComponent<Weapon>();
        currentWeapon = newWeapon;
        this.weaponSO = weaponSO;

    }
    public void HandleZoom()
    {
        if(!weaponSO.CanZoom) return;
        if (starterAssetsInputs.zoom)
        {
            playerFollowCamera.m_Lens.FieldOfView = weaponSO.ZoomAmount;
            zoomVignette.SetActive(true);
            crosshair.SetActive(true);
            firstPersonController.ChangeRotationSpeed(weaponSO.RotationSpeed);

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
