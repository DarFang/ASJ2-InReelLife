
using System.Collections;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    FishingPool fishingPool;
    InputController inputController;
    LootSO currentLoot = null;
    TypingGameController typingGameController;
    
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
    }
    public void StartFishing(FishAreaNumber fishArea)
    {
        //need to add word table here
        currentLoot = fishingPool.GetLoot(fishArea);
        Debug.Log("" + currentLoot.name);
        inputController.playerState = PlayerState.FishingIdle;
        StartCoroutine(LoadingFish());
    }

    internal void CancelFishing()
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

    internal void FinishFishing()
    {
        inputController.playerState = PlayerState.Idle;
    }
}
