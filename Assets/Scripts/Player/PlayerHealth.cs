using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 100;
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] Transform weaponCamera;
    [SerializeField] TMP_Text healthText;
    [SerializeField] Image[] healthBar;

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
        healthText.text = currentHealth.ToString();
        if (currentHealth <= 0)
        {
            healthText.text = "0";
            weaponCamera.parent = null;
            deathVirtualCamera.Priority = gameOverVirtualCameraPriority;
            Destroy(gameObject);
        }
    }
    public void AdjustHealthUI()
    {
        for (int i = 0; i < healthBar.Length; i++)
        {
            if (i < (currentHealth / 10))
            {
                healthBar[i].gameObject.SetActive(true);
            }
            else
            {
                healthBar[i].gameObject.SetActive(false);
            }

        }
      

    }
}
