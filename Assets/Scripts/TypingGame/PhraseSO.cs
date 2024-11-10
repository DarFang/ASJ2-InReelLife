using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Phrase", menuName = " new Phrase", order = 0)]
public class PhraseSO : ScriptableObject 
{
    public string Value = "";
    public Tier tier = Tier.T0;
}

public enum Tier
{
    T0,
    T1,
    T2,
    T3,
}