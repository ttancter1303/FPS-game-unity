using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void LoadNewGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneLoadManager.TargetSceneIndex = currentIndex + 1;
        SceneManager.LoadSceneAsync(6); // Scene 6 là loading scene
    }
    

    public void ExitGame()
    {
        Application.Quit();
    }
}
