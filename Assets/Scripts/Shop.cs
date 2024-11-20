using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    List<ItemDisplay> ShopItemDisplay;
    [SerializeField] ItemDisplay itemDisplay;
    List<ShopItem> shopItems;
    private void Start() 
    {
        ShopItemDisplay = new List<ItemDisplay>();
        InitializeInventory();
    }

    void InitializeInventory()
    {
        foreach(ShopItem shopItem in shopItems)
        {
            ItemDisplay temp = Instantiate(itemDisplay, transform);
            ShopItemDisplay.Add(temp);
        }
    }

}
