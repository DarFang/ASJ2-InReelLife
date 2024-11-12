using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingModule : MonoBehaviour
{
    [SerializeField] bool isFishingAvailable;
    FishAreaNumber fishAreaNumber;
    void Start()
    {
        
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
            GameManager.Instance.StartFishing(fishAreaNumber);
        }
        else
        {
            Debug.Log("You cannot fish at this time");
        }
    }
}
