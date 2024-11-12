using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    FishingPool fishingPool;
    InputController inputController;
    LootSO currentLoot = null;
    
    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            GetReferences();
        }
        else
        {
            Destroy(Instance);
        }
        
    }
    public void GetReferences()
    {
        fishingPool = FindObjectOfType<FishingPool>();
        inputController = FindObjectOfType<InputController>();
    }
    public void StartFishing(FishAreaNumber fishArea)
    {
        currentLoot = fishingPool.GetLoot(fishArea);
        Debug.Log("" + currentLoot.name);
    }
}
