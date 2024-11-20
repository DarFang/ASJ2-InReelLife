using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    List<ShopDisplay> ShopItemDisplay;
    [SerializeField] ShopDisplay shopDisplay;
    [SerializeField] Transform SellTransform;
    [SerializeField] List<ShopItem> shopItems;
    int SelectedIndex = 0;
    private void Start() 
    {
        ShopItemDisplay = new List<ShopDisplay>();
        InitializeInventory();
    }
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (SelectedIndex - 1 >= 0)
            {
                ShopItemDisplay[SelectedIndex].UnSelect();
                SelectedIndex -= 1;
                ShopItemDisplay[SelectedIndex].Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (SelectedIndex + 1 < ShopItemDisplay.Count)
            {
                ShopItemDisplay[SelectedIndex].UnSelect();
                SelectedIndex += 1;
                ShopItemDisplay[SelectedIndex].Select();
            }
        }
    }

    void InitializeInventory()
    {
        SelectedIndex = 0;
        foreach(ShopItem shopItem in shopItems)
        {
            ShopDisplay temp = Instantiate(shopDisplay, SellTransform);
            temp.Initialize(shopItem);
            ShopItemDisplay.Add(temp);
        }
        ShopItemDisplay[SelectedIndex].Select();
    }


}
