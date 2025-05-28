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
        SceneLoadManager.TargetSceneIndex = 1;
        SceneManager.LoadScene(5);
    }
    

    public void ExitGame()
    {
        Application.Quit();
    }
}
