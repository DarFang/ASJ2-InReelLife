using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingModule : MonoBehaviour
{
    [SerializeField] bool isFishingAvailable;
    FishAreaNumber fishAreaNumber;
    InputController inputController;
    void Start()
    {
        inputController = GetComponent<InputController>();
    }
    void Update()
    {
        
    }
    public void EnableFishing(FishAreaNumber fishAreaNumber)
    {
        isFishingAvailable = true;
        this.fishAreaNumber = fishAreaNumber;
    }
    public void DisableFishing()
    {
        isFishingAvailable = false;
    }

    public void AttemptToFish()
    {
        if(isFishingAvailable)
        {
            if(inputController.playerState.Equals(PlayerState.FishingIdle))
            {
                GameManager.Instance.CancelFishing();
            }
            else
            {
                GameManager.Instance.StartFishing(fishAreaNumber);
            }
        }
        else
        {
            Debug.Log("You cannot fish at this time");
        }
    }
}
