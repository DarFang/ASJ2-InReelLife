using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Fish 
{
    [SerializeField] Sprite image;
    [SerializeField] string name = "";
    [SerializeField] int cost = 5;
    public Fish(Sprite image, string name, int cost = 5)
    {
        this.image = image;
        this.name = name;
        this.cost = cost;
    }
    public string Value  { get { return name; } }
    public Sprite Image  { get { return image; } }
    public int Cost  { get { return cost; } }
}
