using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingTrigger : MonoBehaviour
{
    [SerializeField] InputController inputController;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Water"))
        {
            inputController.FishingModule.EnableFishing();
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Water"))
        {
            inputController.FishingModule.DisableFishing();
        }
    }
}
