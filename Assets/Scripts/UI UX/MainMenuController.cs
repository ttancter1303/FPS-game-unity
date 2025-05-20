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
        SceneManager.LoadScene(1);
    }
    
    public void Continue()
    {
        if (LoadSystem.HasSaveFile())
        {
            SceneManager.LoadScene(1); // trong scene đó sẽ load data sau khi player spawn
        }
        else
        {
            Debug.Log("Không có dữ liệu save. Bắt đầu game mới.");
            LoadNewGame();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
