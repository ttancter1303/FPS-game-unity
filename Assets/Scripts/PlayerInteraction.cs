using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float playerReach = 3f;
    Interactable currentInteracable;

    void Update()
    {
        CheckInteraction();
        if(Input.GetKeyDown(KeyCode.E) && currentInteracable != null)
        {
            currentInteracable.Interact();
        }
    }
    void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        
        // khi player bắn raycast trong phạm vi của player reach
        if(Physics.Raycast(ray, out hit,playerReach))
        {
            if(hit.collider.tag == "Interactable") // kiểm tra vật có gắn tag Interacable không
            {
                Debug.Log("Hit Interactable");
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();
               
                if(currentInteracable && newInteractable != currentInteracable)
                {
                    currentInteracable.DisableOutline();
                }
                if (newInteractable.enabled)
                {
                    SetNewCurrentInteracable(newInteractable);
                }
                else // nếu như không có thì tắt
                {
                    DisableCurrentInteracable();
                }
            }
            else // nếu như không có thì tắt
            {
                DisableCurrentInteracable();
            }
        }
    }
    void SetNewCurrentInteracable(Interactable newIneracable)
    {
        currentInteracable = newIneracable;
        currentInteracable.EnableOutline();
        HUDController.instance.EnableInteractionText(currentInteracable.message);
    }
    void DisableCurrentInteracable()
    {
        HUDController.instance.DisableInteractionText();
        if (currentInteracable != null)
        {
            currentInteracable.DisableOutline();
            currentInteracable = null;
        }
    }
}
