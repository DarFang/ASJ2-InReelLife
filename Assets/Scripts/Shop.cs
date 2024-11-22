using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    List<ShopDisplay> ShopItemDisplay;
    [SerializeField] ShopDisplay shopDisplayPrefab;
    [SerializeField] Transform SellTransform;
    [SerializeField] List<ShopItem> shopItems;
    [SerializeField] TextMeshProUGUI PlayerBank;
    [SerializeField] GameObject shopCanvas;
    [SerializeField] ShopNPC shopNPC;
    int SelectedIndex = 0;
    private void Start() 
    {
        ShopItemDisplay = new List<ShopDisplay>();
        InitializeInventory();
        UpdatePlayerBank();
        CloseShop();
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
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            AttempToPurchaseItem();
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SellAll();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            shopNPC.Interact();
        }
    }

    private void AttempToPurchaseItem()
    {
        shopItems[SelectedIndex].AttemptToPurchaseItem();
        UpdatePlayerBank();
    }

    void UpdatePlayerBank()
    {
        PlayerBank.text = GameManager.Instance.GetPlayerBank().ToString();
    }

    void InitializeInventory()
    {
        SelectedIndex = 0;
        foreach(ShopItem shopItem in shopItems)
        {
            ShopDisplay temp = Instantiate(shopDisplayPrefab, SellTransform);
            temp.Initialize(shopItem);
            ShopItemDisplay.Add(temp);
        }
        ShopItemDisplay[SelectedIndex].Select();
    }
    void SellAll()
    {
        GameManager.Instance.SellAllInventory();
        UpdatePlayerBank();
    }
    public void OpenShop()
    {
        enabled = true;
        shopCanvas.SetActive(true);
    }
    public void CloseShop()
    {
        enabled = false;
        shopCanvas.SetActive(false);
    }
}
