using System;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    public void ShowMessage()
    {
        messageText.gameObject.SetActive(true);
        CancelInvoke();
        Invoke(nameof(HideMessage), 10f);
    }

    void HideMessage()
    {
        messageText.gameObject.SetActive(false);
    }
}