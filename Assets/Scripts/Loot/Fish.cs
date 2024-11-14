using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Fish 
{
    [SerializeField] RawImage image;
    [SerializeField] string value = "";
    public Fish(RawImage image, string value)
    {
        this.image = image;
        this.value = value;
    }
}
