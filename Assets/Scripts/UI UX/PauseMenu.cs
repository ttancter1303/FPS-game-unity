using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject gameOverUI;

    public static bool IsPaused = false;
    PlayerHealth playerHealth;
    private bool gameOverShown = false;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
                ResumeGame();
            else
                PauseGame();
        }else if(playerHealth.currentHealth<=0)
        {
            ShowGameOverUI();
        }
        
    }

    public void ShowGameOverUI()
    {
        if (gameOverShown) return;

        Time.timeScale = 0f;
        IsPaused = true;
        gameOverUI.SetActive(true);
        gameOverShown = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(RestartSceneAfterDelay());
    }

    private IEnumerator RestartSceneAfterDelay()
    {
        yield return new WaitForSecondsRealtime(3f);

        Time.timeScale = 1f;
        IsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void PauseGame()
    {
        Time.timeScale = 0f;
        IsPaused = true;
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        IsPaused = false;
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }





    public void QuitGame()
    {
        Application.Quit();
    }
}
