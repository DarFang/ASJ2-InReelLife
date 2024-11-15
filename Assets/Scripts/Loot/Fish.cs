using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Fish 
{
    [SerializeField] Sprite image;
    [SerializeField] string value = "";
    public Fish(Sprite image, string value)
    {
        this.image = image;
        this.value = value;
    }
    public string Value  { get { return value; } }
    public Sprite Image  { get { return image; } }
}
