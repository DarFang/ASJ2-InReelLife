
using System.Collections;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    FishingPool fishingPool;
    InputController inputController;
    LootSO currentLoot = null;
    TypingGameController typingGameController;
    public Dialog dialog { get; private set;}
    
    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            GetReferences();
        }
        else
        {
            Destroy(Instance);
        }
        
    }
    public void GetReferences()
    {
        fishingPool = FindObjectOfType<FishingPool>();
        inputController = FindObjectOfType<InputController>();
        typingGameController = FindObjectOfType<TypingGameController>();
        dialog = FindObjectOfType<Dialog>();
    }
    public void StartFishing(FishAreaNumber fishArea)
    {
        //need to add word table here
        currentLoot = fishingPool.GetLoot(fishArea);
        Debug.Log("" + currentLoot.name);
        inputController.playerState = PlayerState.FishingIdle;
        StartCoroutine(LoadingFish());
    }

    public void CancelFishing()
    {
        inputController.playerState = PlayerState.Idle;
        Debug.Log("cancellingFishing");
    }

    IEnumerator LoadingFish()
    {
        float randomTime = Random.Range(0f,1f);
        yield return new WaitForSeconds(randomTime);
        if(inputController.playerState == PlayerState.FishingIdle)
        {
            inputController.playerState = PlayerState.FishingFish;
            typingGameController.gameObject.SetActive(true);
        }
    }

    public void FinishFishing()
    {
        inputController.playerState = PlayerState.Idle;
        EnablePlayerInput();
    }
    public void DisablePlayerInput()
    {
        inputController.enabled = false;
    }
    public void EnablePlayerInput()
    {
        inputController.enabled = true;
    }

    public bool AttemptPurchaseItem(ShopItem shopItem)
    {
        Inventory inventory = inputController.GetComponent<Inventory>();
        return inventory.AttemptToAddItem(shopItem);
    }
    public void SellAllInventory()
    {
        inputController.GetComponent<Inventory>().SellAllInventory();
    }
    public int GetPlayerBank()
    {
        return inputController.GetComponent<Inventory>().Money;
    }
}
