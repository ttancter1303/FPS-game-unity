using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] TMP_Text interactionText;
    public static HUDController instance;
    private void Awake()
    {
        instance = this;
    }
    public void EnableInteractionText(string text)
    {
        interactionText.text = text + " E ";
        interactionText.gameObject.SetActive(true);
    }
    public void DisableInteractionText()
    {
        interactionText.gameObject.SetActive(false);
    }

}
