using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    Inventory inventory;

    [SerializeField] ItemDisplay itemDisplay;
    [SerializeField] List<ItemDisplay> FishInventoryDisplay;
    [SerializeField] Transform FishInventoryPivot;
    [SerializeField] GameObject MainInventory;
    bool isActive = false;
    int Selected = 0;
    int gridSize = 5;
    void Awake()
    {
        MainInventory.SetActive(isActive);
    }
    private void Start() 
    {
        inventory = FindObjectOfType<Inventory>();
        InitializeInventory();
    }
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isActive = !isActive;
            MainInventory.SetActive(isActive);
            LoadInventory();
            if(isActive)
            {
                GameManager.Instance.DisablePlayerInput();
            }
            else
            {
                GameManager.Instance.EnablePlayerInput();
            }
        }
        if(!isActive) return;
        if (Input.GetKeyDown(KeyCode.D))
        {
            if ((Selected + 1) % gridSize != 0 && (Selected + 1) < inventory.InventoryLimit)
            {
                FishInventoryDisplay[Selected].UnSelect();
                Selected++;
                FishInventoryDisplay[Selected].Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (Selected % gridSize != 0)
            {
                FishInventoryDisplay[Selected].UnSelect();
                Selected--;
                FishInventoryDisplay[Selected].Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (Selected - gridSize >= 0)
            {
                FishInventoryDisplay[Selected].UnSelect();
                Selected -= gridSize;
                FishInventoryDisplay[Selected].Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (Selected + gridSize < gridSize * gridSize && Selected + gridSize < inventory.InventoryLimit)
            {
                FishInventoryDisplay[Selected].UnSelect();
                Selected += gridSize;
                FishInventoryDisplay[Selected].Select();
            }
        }
    }
    public void UpdateSelected()
    {

    }
    void InitializeInventory()
    {
        for (int i = 0; i < inventory.InventoryFishSize; i++)
        {
            ItemDisplay temp = Instantiate(itemDisplay, FishInventoryPivot);
            FishInventoryDisplay.Add(temp);
        }
    }

    void LoadInventory()
    {
        Selected = 0;
        for (int i = 0; i < inventory.InventoryLimit; i++)
        {
            if(inventory.fishList.Count > i)
            {
                FishInventoryDisplay[i].UpdateDisplay(FishToSpriteMap.Instance.ReturnSprite(inventory.fishList[i].Value));
            }
            else
            {
                FishInventoryDisplay[i].ClearDisplay();
            }
        }
        for (int i = inventory.InventoryLimit; i< inventory.InventoryFishSize; i++)
        {
            FishInventoryDisplay[i].DisableDisplay();
        }
        FishInventoryDisplay[Selected].Select();
    }
}
