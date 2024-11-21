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
    public PlayerState playerState = PlayerState.Idle;
    public bool isMoving { get; private set; }

    private void Update() 
    {
        if(GameManager.Instance.dialog.IsRunning) return;
        if(playerState == PlayerState.FishingFish) return;
        if(fishingModule != null && Input.GetKeyDown(KeyCode.R))
        {
            fishingModule.AttemptToFish();
        }
        if(playerState == PlayerState.FishingIdle) return;
        if(interactModule != null && Input.GetKeyDown(KeyCode.F))
        {
            interactModule.AttemptToInteract();

        }


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


    }
    public FishingModule FishingModule => fishingModule;
    public InteractModule InteractModule => interactModule;
}
public enum PlayerState
{
    Idle,
    Waling,
    Running,
    Crouching,
    FishingIdle,
    FishingFish,
    Interacting,
}
