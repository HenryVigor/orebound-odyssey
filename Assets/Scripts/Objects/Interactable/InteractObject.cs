using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : BaseInteractableObject
{
    public override void Use()
    {
        if (isInteractable)
        {
            isInteractable = false;

            if (timesInteracted < maxInteractions)
            {

                // This method gets an override in other interactable scripts to create different behavior
                Interaction();

                if (limitInteractions)
                {
                    timesInteracted++;
                }

            }

            Invoke("ResetInteract", interactCooldown);
        }
    }


    private void ResetInteract()
    {
        isInteractable = true;
    }

    public override void Interaction()
    {
        if (interactState == 0)
        {
            interactState = 1;
            Debug.Log("Interact Object Activated");
        }
        else
        {
            interactState = 0;
            Debug.Log("Interact Object Deactivated");
        }
    }

    public override string GetInteractName()
    {
        return itemName;
    }

    public override string GetInteractDesc()
    {
        return itemText;
    }

    public override string GetInteractAction()
    {
        return itemAction;
    }

}
