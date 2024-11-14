using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Inventory : MonoBehaviour
{
    public List<Fish> fishList;
    public RawImage image;
    private void Start() {
        fishList = new List<Fish>();
        fishList.Add(new Fish(image, "string"));
    }
}
