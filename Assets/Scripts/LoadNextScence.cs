using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScence : Interactable
{
    public override void OnInteract()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
