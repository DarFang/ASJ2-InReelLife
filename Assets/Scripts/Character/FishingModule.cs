using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingModule : MonoBehaviour
{
    [SerializeField] bool isFishingAvailable;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void EnableFishing()
    {
        isFishingAvailable = true;
    }
    public void DisableFishing()
    {
        isFishingAvailable = false;
    }

    public void AttemptToFish()
    {
        if(isFishingAvailable)
        {
            Debug.Log("You can fish");
        }
        else
        {
            Debug.Log("You cannot fish at this time");
        }
    }
}
