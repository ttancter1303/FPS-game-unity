using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScence : Interactable
{
    public override void OnInteract()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex >= 6) return;

        SceneLoadManager.TargetSceneIndex = currentIndex + 1;
        Debug.Log("Đang set TargetSceneIndex = " + SceneLoadManager.TargetSceneIndex);
        SceneManager.LoadSceneAsync(6); // Scene 6 là loading scene
    }
}
