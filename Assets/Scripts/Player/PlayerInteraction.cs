﻿using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float playerReach = 3f;
    Interactable currentInteractable;

    void Update()
    {
        CheckInteraction();

        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, playerReach))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();

                if (newInteractable != null)
                {
                    if (currentInteractable && newInteractable != currentInteractable)
                    {
                        currentInteractable.DisableOutline();
                    }

                    if (newInteractable.enabled)
                    {
                        SetNewCurrentInteractable(newInteractable);
                    }
                    else
                    {
                        DisableCurrentInteractable();
                    }
                }
                else
                {
                    DisableCurrentInteractable();
                }

            }
            else
            {
                DisableCurrentInteractable();
            }
        }
        else
        {
            DisableCurrentInteractable();
        }
    }

    void SetNewCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
        HUDController.instance.EnableInteractionText(currentInteractable.message);
    }


    void DisableCurrentInteractable()
    {
        HUDController.instance.DisableInteractionText();

        if (currentInteractable != null)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }
}
