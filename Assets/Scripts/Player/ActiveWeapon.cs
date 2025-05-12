using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] FirearmWeaponSO startingWeaponSO;
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] Camera weaponCamera;
    [SerializeField] GameObject zoomVignette;
    [SerializeField] GameObject crosshair;
    [SerializeField] TMP_Text ammoText;
    [SerializeField] GameObject weaponSlot1;
    [SerializeField] GameObject weaponSlot2;
    [SerializeField] GameObject weaponSlot3;
    [SerializeField] GameObject weaponSlot4;


    WeaponSO[] availableWeapons = new FirearmWeaponSO[4];
    FirearmWeaponSO currentWeaponSO;

    StarterAssetsInputs starterAssetsInputs;
    FirearmWeapon currentWeapon;
    FirstPersonController firstPersonController;


    float timeSinceLastShot = 0f;
    int[] weaponAmmos = new int[4];

    float timeCooldown = 0f;
    float defaultFOV;
    float defaultRotationSpeed;
    int currentAmmo;
    int weaponIndex = 0;

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();

    }

    private void Start()
    {
        SwitchWeapon(startingWeaponSO);
        AddWeapon(startingWeaponSO);
        defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
        AdjustAmmo(startingWeaponSO.MagazineSize);

    }

    void Update()
    {
        timeCooldown += Time.deltaTime;
        HandleShoot();
        HandleZoom();
        HandleSwitchWeapon();
    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount;

        if (currentAmmo > currentWeaponSO.MagazineSize)
        {
            currentAmmo = currentWeaponSO.MagazineSize;
        }

        ammoText.text = currentAmmo.ToString("D2");
    }


    void HandleShoot()
    {
        timeSinceLastShot += Time.deltaTime;

        if (!starterAssetsInputs.shoot) return;

        if (timeSinceLastShot >= currentWeaponSO.FireRate && currentAmmo > 0)
        {
            currentWeapon.Attack();

            timeSinceLastShot = 0f;
            AdjustAmmo(-1);
        }

        if (!currentWeaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }
    }
    public void HandleSwitchWeapon()
    {
        if (starterAssetsInputs.switchWeapon1)
        {
            TrySwitchWeapon(0,weaponSlot1);
            
            starterAssetsInputs.switchWeapon1 = false;
        }
        else if (starterAssetsInputs.switchWeapon2)
        {
            TrySwitchWeapon(1, weaponSlot2);
            starterAssetsInputs.switchWeapon2 = false;
        }
        else if (starterAssetsInputs.switchWeapon3)
        {
            TrySwitchWeapon(2, weaponSlot3);
            starterAssetsInputs.switchWeapon3 = false;
        }
        else if (starterAssetsInputs.switchWeapon4)
        {
            TrySwitchWeapon(3, weaponSlot4);
            starterAssetsInputs.switchWeapon4 = false;
        }
    }
    void TrySwitchWeapon(int index, GameObject selectedSlot)
    {
        // Reset màu tất cả slot về trắng
        ResetAllSlotColors();

        if (index < 0 || index >= availableWeapons.Length) return;

        var targetWeapon = availableWeapons[index];
        if (targetWeapon == null) return;

        if (targetWeapon == currentWeaponSO)
        {
            Debug.Log("Already holding: " + targetWeapon.name);
            return;
        }

        Debug.Log("Switching to: " + targetWeapon.name);

        // Tô đỏ slot mới
        selectedSlot.GetComponent<Image>().color = Color.red;

        SwitchWeapon(targetWeapon);
    }
    void ResetAllSlotColors()
    {
        weaponSlot1.GetComponent<Image>().color = Color.white;
        weaponSlot2.GetComponent<Image>().color = Color.white;
        weaponSlot3.GetComponent<Image>().color = Color.white;
        weaponSlot4.GetComponent<Image>().color = Color.white;
    }



    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
            currentWeapon = null;
        }

        if (weaponSO is FirearmWeaponSO firearmWeaponSO)
        {
            GameObject instance = Instantiate(firearmWeaponSO.WeaponPrefab, transform);
            currentWeapon = instance.GetComponent<FirearmWeapon>();
            currentWeaponSO = firearmWeaponSO;

            currentAmmo = firearmWeaponSO.MagazineSize;
            ammoText.text = currentAmmo.ToString("D2");

            crosshair.SetActive(!firearmWeaponSO.CrosshairOff);
        }
        else
        {
            Debug.LogWarning("Unsupported weapon type.");
        }
    }


    public bool AddWeapon(WeaponSO newWeapon)
    {
        // Không thêm nếu đã có trong danh sách
        for (int i = 0; i < availableWeapons.Length; i++)
        {
            if (availableWeapons[i] == newWeapon)
            {
                Debug.Log("Weapon already in inventory: " + newWeapon.name);
                return false;
            }
        }

        // Thêm vào slot trống đầu tiên
        for (int i = 0; i < availableWeapons.Length; i++)
        {
            if (availableWeapons[i] == null)
            {
                availableWeapons[i] = newWeapon;
                Debug.Log("Weapon added: " + newWeapon.name);
                return true;
            }
        }

        Debug.Log("Weapon inventory full!");
        return false;
    }



    public void DebugPickup()
    {
        for (int i = 0; i < availableWeapons.Length; i++)
        {
            var item = availableWeapons[i];
            if (item != null)
            {
                Debug.Log($"Slot {i + 1}: {item.name}");
            }
            else
            {
                Debug.Log($"Slot {i + 1}: Empty");
            }
        }
    }

    public void HandleZoom()
    {
        if (!currentWeaponSO.CanZoom) return;

        if (starterAssetsInputs.zoom)
        {
            playerFollowCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
            weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;
            
            zoomVignette.SetActive(true);
            crosshair.SetActive(true);
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.RotationSpeed);
        }
        else
        {
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
            crosshair.SetActive(!currentWeaponSO.CrosshairOff);
            playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
            weaponCamera.fieldOfView = defaultFOV;
            zoomVignette.SetActive(false);
        }
    }
}