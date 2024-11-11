using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    private Vector3 moveDirection;
    [SerializeField] MovementModule movementModule;
    [SerializeField] FishingModule fishingModule;
    [SerializeField] InteractModule interactModule;
    public bool isMoving { get; private set; }
    private void Update() {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.z = Input.GetAxisRaw("Vertical");

        if(movementModule != null)
        {
            isMoving = moveDirection != Vector3.zero;
            if(isMoving)
            {
                movementModule.MoveCharacter(moveDirection);
            }
        }
        if(interactModule != null && Input.GetKeyDown(KeyCode.F))
        {
            isMoving = moveDirection != Vector3.zero;
            Debug.Log("Interacting");
        }
        if(fishingModule != null && Input.GetKeyDown(KeyCode.R))
        {
            fishingModule.AttemptToFish();
        }


    }
    public FishingModule FishingModule => fishingModule;
}
