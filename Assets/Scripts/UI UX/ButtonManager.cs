using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private PauseMenu pauseMenu;
    private void Awake()
    {
        if (pauseMenu == null)
            pauseMenu = FindObjectOfType<PauseMenu>();
    }
    public void Resume()
    {
        pauseMenu.ResumeGame();
    }

    public void Exit()
    {
        pauseMenu.QuitGame();
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
