using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : ScriptableObject
{
    public int Cost {get{return cost;}}
    [SerializeField] int cost = 1;
    public void PurchaseItem()
    {
        GameManager.Instance.PurchaseItem(this);
    }
}
