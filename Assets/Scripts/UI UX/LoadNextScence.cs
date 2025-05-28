using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScence : Interactable
{
    public override void OnInteract()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex >= 4) return;

        SceneLoadManager.TargetSceneIndex = currentIndex + 1;
        SceneManager.LoadScene(5); // Scene 5 là loading scene
    }

}
