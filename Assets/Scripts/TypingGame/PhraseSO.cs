using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Phrase", menuName = " new Phrase", order = 0)]
public class PhraseSO : ScriptableObject 
{
    public string Value = "";
    public string Tier = "Tier0";
    public string Event;
    public string Location;
}

// public enum Tier
// {
//     Tier0,
//     Tier1,
//     Tier2,
//     Tier3,
// }