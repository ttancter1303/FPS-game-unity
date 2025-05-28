using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreenController : MonoBehaviour
{
    public Slider progressBar;

    void Start()
    {
        Debug.Log("LoadingScreenController Start");
        Debug.Log("TargetSceneIndex: " + SceneLoadManager.TargetSceneIndex);
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        int targetIndex = SceneLoadManager.TargetSceneIndex;

        if (targetIndex < 0 || targetIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError("Scene index không hợp lệ: " + targetIndex);
            yield break;
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(targetIndex);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (progressBar != null)
                progressBar.value = progress;

            // Kiểm tra khi đã gần xong (0.9f) để cho phép chuyển
            if (operation.progress >= 0.9f)
            {
                Debug.Log("Scene gần load xong, chuẩn bị activate");
                yield return new WaitForSeconds(0.5f); // thời gian để hiển thị full progress
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}