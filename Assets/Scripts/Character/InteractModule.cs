using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractModule : MonoBehaviour
{
    
    NPCInteractTrigger interact = null;
    public void DisableInteraction()
    {
        interact = null;
    }

    public void EnableInteraction(NPCInteractTrigger interact)
    {
        this.interact = interact;
    }

    public void AttemptToInteract()
    {
        if(interact != null)
        {
            interact.Interact();
        }
        else
        {
            Debug.Log("nothing to interact with");
        }
    }
}
