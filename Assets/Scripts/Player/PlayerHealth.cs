using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 100;
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] Transform weaponCamera;
    [SerializeField] Slider healthSlider;

    public int currentHealth;
    int gameOverVirtualCameraPriority = 20;
    public static PlayerHealth Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("ActiveWeapon.Instance đã tồn tại! Có thể có nhiều đối tượng ActiveWeapon.");
        }
        currentHealth = startingHealth;
        AdjustHealthUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        AdjustHealthUI();
        if (currentHealth <= 0)
        {
            healthSlider.value = 0;
            weaponCamera.parent = null;
            deathVirtualCamera.Priority = gameOverVirtualCameraPriority;
            Destroy(gameObject);
        }
    }
    public void AdjustHealthUI()
    {
        healthSlider.value = currentHealth;
    }
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveSystem.SaveGameState();
        }
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SaveGameState();
    }
}
