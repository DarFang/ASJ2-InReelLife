using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Inventory : MonoBehaviour
{
    public List<Fish> fishList;
    public Sprite image;
    int inventoryFishSize = 25;
    int inventoryLimit = 10;
    public int InventoryFishSize {get{return inventoryFishSize;}}
    public int InventoryLimit {get{return inventoryLimit;}}
    [SerializeField] int money = 0;
    public int Money {get{return money;}}
    private void Start() {
        fishList = new List<Fish>();
        AddFish();
    }
    void AddFish()
    {
        if(inventoryLimit<=fishList.Count)
        {
            Debug.Log("cannot add anymore fish");
            return;   
        }
        fishList.Add(new Fish(image, "string"));
        Debug.Log("addfish");
    }
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Slash)) 
        {
            AddFish();
        }
        if(Input.GetKeyDown(KeyCode.Backslash))
        {
            if(fishList.Count > 0)
            {
                Debug.Log("remove fish");
                fishList.RemoveAt(0);
            }
        }
    }
    public bool PeekInvenotry(string value)
    {
        foreach (var item in fishList)
        {
            if (item.Value.Equals(value))
            {
                return true;
            }
        }
        return false;
    }
    public Fish GrabItem(string value)
    {
        foreach (var item in fishList)
        {
            if (item.Value.Equals(value))
            {
                fishList.Remove(item);
                return item;
            }
        }
        return null;
    }
    public bool AttemptToAddItem(ShopItem shopitem)
    {
        if (shopitem.Cost > money)
        {
            Debug.Log("not enough currency");
            return false;
        }
        else
        {
            money -= shopitem.Cost;
            AddListItem(shopitem);
            return true;
        }
    }

    private void AddListItem(ShopItem shopitem)
    {
        
    }
    public void SellAllInventory()
    {
        if (fishList.Count <= 0) return;
        int TotalCost = 0;
        foreach (var item in fishList)
        {
            TotalCost += item.Cost;
        }
        fishList = new List<Fish>();
        money += TotalCost;
    }
}
