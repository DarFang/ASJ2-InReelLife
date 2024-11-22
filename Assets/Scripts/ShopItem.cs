using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ShopItem", menuName = "ShopItem_", order = 3)]
public class ShopItem : ScriptableObject
{
    public int Cost {get{return cost;}}
    [SerializeField] int cost = 1;
    [SerializeField] public Sprite Sprite;
    [SerializeField] public String Name;
    public bool AttemptToPurchaseItem()
    {
        return GameManager.Instance.AttemptPurchaseItem(this);
    }
}
