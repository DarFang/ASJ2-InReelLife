using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Loot", menuName = "Loot_", order = 0)]
public class LootSO : ScriptableObject 
{
    public string Value = "fish";
    public int cost = 5;
}