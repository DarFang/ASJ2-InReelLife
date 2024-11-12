using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPool : MonoBehaviour
{
    [SerializeField] List<LootSO> Pond1Loot;

    public LootSO GetLoot(FishAreaNumber number)
    {
        switch (number)
        {
            case FishAreaNumber.Area1:
                return Pond1Loot[Random.Range(0,Pond1Loot.Count)];
            // case FishAreaNumber.Area2:
            //     return Pond1Loot[Random.Range(0,Pond2Loot.Count)];
        }
        return null;

    }
    
}
