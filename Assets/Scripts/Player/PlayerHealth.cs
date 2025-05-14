using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 100;
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] Transform weaponCamera;
    [SerializeField] Slider healthSlider;

    int currentHealth;
    int gameOverVirtualCameraPriority = 20;

    void Awake()
    {
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
}
