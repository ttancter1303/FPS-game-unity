using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour
{
    Outline outline;
    public string message;
    public UnityEvent onInteraction; 

    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void Interact()
    {
        OnInteract();
        onInteraction?.Invoke();

        
    }
    public abstract void OnInteract();
    public void DisableOutline()
    {
        if (outline != null)
            outline.enabled = false;
    }

    public void EnableOutline()
    {
        if (outline != null)
            outline.enabled = true;
    }
}
