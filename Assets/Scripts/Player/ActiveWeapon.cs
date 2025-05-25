using System;
using System.Linq;
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
    [SerializeField] Image[] weaponSlots;
    [SerializeField] Image[] weaponIconSlots;
    [SerializeField] GameObject pauseMenu;


    public WeaponSO[] availableWeapons = new FirearmWeaponSO[4];
    public FirearmWeaponSO currentWeaponSO;
    StarterAssetsInputs starterAssetsInputs;
    FirearmWeapon currentWeapon;
    FirstPersonController firstPersonController;

    public static ActiveWeapon Instance;
    private Color originalSlotColor;
    
    float timeSinceLastShot = 0f;
    float timeCooldown = 0f;
    float defaultFOV;
    float defaultRotationSpeed;
    public int[] weaponAmmos = new int[4];
    int currentAmmo;
    int weaponIndex = 0;
    



    void Awake()
    {
        
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("ActiveWeapon.Instance đã tồn tại! Có thể có nhiều đối tượng ActiveWeapon.");
        }

        originalSlotColor = weaponSlots[0].GetComponent<Image>().color;

    }

    private void Start()
    {
        DisableAllWeaponSlot();
        SwitchWeapon(startingWeaponSO);
        AddWeapon(startingWeaponSO);
        weaponSlots[0].GetComponent<Image>().color = Color.white;
        defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
        AdjustAmmo(startingWeaponSO.MagazineSize);
        // LoadGameDataAndApply();

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
        int index = GetWeaponIndex(currentWeaponSO);

        if (index != -1 && currentWeaponSO is FirearmWeaponSO firearm)
        {
            if (currentAmmo > firearm.MagazineSize)
                currentAmmo = firearm.MagazineSize;

            weaponAmmos[index] = currentAmmo;
        }

        ammoText.text = currentAmmo.ToString("D2");
    }
    int GetWeaponIndex(WeaponSO weapon)
    {
        for (int i = 0; i < availableWeapons.Length; i++)
        {
            if (availableWeapons[i] == weapon)
                return i;
        }
        return -1;
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

    #region Switch weapon
    public void HandleSwitchWeapon()
    {
        if (starterAssetsInputs.switchWeapon1)
        {
            TrySwitchWeapon(0, weaponSlots[0]);
            
            starterAssetsInputs.switchWeapon1 = false;
        }
        else if (starterAssetsInputs.switchWeapon2)
        {
            TrySwitchWeapon(1, weaponSlots[1]);
            starterAssetsInputs.switchWeapon2 = false;
        }
        else if (starterAssetsInputs.switchWeapon3)
        {
            TrySwitchWeapon(2, weaponSlots[2]);
            starterAssetsInputs.switchWeapon3 = false;
        }
        else if (starterAssetsInputs.switchWeapon4)
        {
            TrySwitchWeapon(3, weaponSlots[3]);
            starterAssetsInputs.switchWeapon4 = false;
        }
    }
    void TrySwitchWeapon(int index, Image selectedSlot)
    {
        // Reset màu tất cả slot về trắng
        ResetAllSlotColors();

        if (index < 0 || index >= availableWeapons.Length) return;

        var targetWeapon = availableWeapons[index];
        if (targetWeapon == null) return;

        if (targetWeapon == currentWeaponSO)
        {
            selectedSlot.GetComponent<Image>().color = Color.white;
            Debug.Log("Already holding: " + targetWeapon.name);
            return;
        }

        Debug.Log("Switching to: " + targetWeapon.name);


        selectedSlot.GetComponent<Image>().color = Color.white;

        SwitchWeapon(targetWeapon);
    }
    void ResetAllSlotColors()
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            weaponSlots[i].GetComponent<Image>().color = originalSlotColor;
        }
    }

    void DisableAllWeaponSlot()
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            weaponSlots[i].enabled = false;
            weaponIconSlots[i].enabled = false;
        }
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

            int index = GetWeaponIndex(currentWeaponSO);
            currentAmmo = (index != -1) ? weaponAmmos[index] : firearmWeaponSO.MagazineSize;

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
                weaponSlots[i].enabled = true;
                weaponIconSlots[i].enabled = true;

                availableWeapons[i] = newWeapon;
                if (newWeapon is FirearmWeaponSO firearm)
                {
                    weaponIconSlots[i].preserveAspect = true;

                    weaponIconSlots[i].sprite = firearm.weaponIcon;
                    weaponAmmos[i] = firearm.MagazineSize;
                }
                Debug.Log("Weapon added: " + newWeapon.name);
                return true;
            }
        }

        Debug.Log("Weapon inventory full!");
        return false;
    }
    

    #endregion
    

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

    // private void OnApplicationPause(bool pauseStatus)
    // {
    //     if (pauseStatus)
    //     {
    //         SaveSystem.SaveGameState();
    //     }
    // }
    //
    //
    // private void OnApplicationQuit()
    // {
    //     SaveSystem.SaveGameState();
    // }

    // public static void LoadGameDataAndApply()
    // {
    //     SaveData saveData = LoadSystem.LoadGameData();
    //     if (saveData == null || saveData.weaponData == null)
    //     {
    //         Debug.LogWarning("No save data found!");
    //         return;
    //     }
    //
    //     var playerData = saveData.weaponData;
    //     var activeWeapon = ActiveWeapon.Instance;
    //     if (activeWeapon == null)
    //     {
    //         Debug.LogError("ActiveWeapon not found!");
    //         return;
    //     }
    //
    //     // Xóa hết vũ khí hiện tại
    //     for (int i = 0; i < activeWeapon.availableWeapons.Length; i++)
    //     {
    //         activeWeapon.availableWeapons[i] = null;
    //         activeWeapon.weaponAmmos[i] = 0;
    //     }
    //
    //     // Load lại danh sách weapon
    //     for (int i = 0; i < playerData.weaponIDs.Count; i++)
    //     {
    //         string weaponID = playerData.weaponIDs[i];
    //         int ammo = playerData.weaponAmmos.Count > i ? playerData.weaponAmmos[i] : 0;
    //
    //         FirearmWeaponSO weapon = FindWeaponByID(weaponID);
    //         if (weapon != null)
    //         {
    //             activeWeapon.availableWeapons[i] = weapon;
    //             activeWeapon.weaponAmmos[i] = ammo;
    //         }
    //     }
    //
    //     // Set lại current weapon
    //     FirearmWeaponSO currentWeapon = FindWeaponByID(playerData.currentWeaponID);
    //     if (currentWeapon != null)
    //     {
    //         activeWeapon.SwitchWeapon(currentWeapon);
    //     }
    //
    //     Debug.Log("Game data loaded successfully.");
    // }
    // public static FirearmWeaponSO FindWeaponByID(string id)
    // {
    //     return SaveSystem.allWeapons.FirstOrDefault(w => w.WeaponID == id);
    // }

}