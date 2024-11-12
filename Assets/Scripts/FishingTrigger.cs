using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingTrigger : MonoBehaviour
{
    [SerializeField] InputController inputController;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Water"))
        {
            inputController.FishingModule.EnableFishing(other.GetComponent<FishingPond>().fishAreaNumber);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Water"))
        {
            inputController.FishingModule.DisableFishing();
        }
    }
}
public enum FishAreaNumber
{
    Area1,
    Area2,
    Area3,
    Area4,

}
