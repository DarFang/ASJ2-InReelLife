using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCInteractTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<InteractModule>().EnableInteraction(this);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<InteractModule>().DisableInteraction();
        }
    }
    public abstract void Interact();

}
