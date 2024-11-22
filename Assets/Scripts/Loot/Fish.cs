using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Fish 
{
    [SerializeField] string name = "";
    [SerializeField] int cost = 5;
    public Fish(string name, int cost = 5)
    {
        this.name = name;
        this.cost = cost;
    }
    public string Value  { get { return name; } }
    public int Cost  { get { return cost; } }
}
