using System;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public GameObject FirstAidIcon;
    [SerializeField] int startingHealth = 100;
    [SerializeField] Transform weaponCamera;
    [SerializeField] Slider healthSlider;
    
    AudioSource audioSource;
    public bool hasFirstAid = false;
    private int firtAidHealthAmount = 60;
    public int currentHealth;
    int gameOverVirtualCameraPriority = 20;
    public static PlayerHealth Instance;

    void Awake()
    {
        FirstAidIcon.SetActive(false);
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
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Healing();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        AdjustHealthUI();
        if (currentHealth <= 0)
        {
            healthSlider.value = 0;
            weaponCamera.parent = null;
            Destroy(gameObject);
        }
    }
    public void AdjustHealthUI()
    {
        healthSlider.value = currentHealth;
    }
    // private void OnApplicationPause(bool pauseStatus)
    // {
    //     if (pauseStatus)
    //     {
    //         SaveSystem.SaveGameState();
    //     }
    // }
    //
    // private void OnApplicationQuit()
    // {
    //     SaveSystem.SaveGameState();
    // }

    public void Healing()
    {
        if (Input.GetKeyDown(KeyCode.F) && hasFirstAid)
        {
            currentHealth += firtAidHealthAmount;
            if (currentHealth >= startingHealth)
            {
                currentHealth = startingHealth;
            }
            Debug.Log("Player healed for " + firtAidHealthAmount + " health points");
            FirstAidIcon.SetActive(false);
            hasFirstAid = false;
            AdjustHealthUI();
            audioSource.Play();
        }
    }
}
