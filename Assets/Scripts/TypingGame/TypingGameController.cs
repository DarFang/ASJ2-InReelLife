using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TypingGameController : MonoBehaviour
{
    [SerializeReference] PhraseDisplay phraseDisplayPrefab;
    PhraseDisplay currentPhraseDisplay;
    [SerializeField] PhraseSO phraseSO;
    [SerializeField] List<PhraseSO> LootTable;
    [SerializeField] List<PhraseSO> PopulatedPhrases;
    List<PhraseDisplay> PhraseDisplays  = new List<PhraseDisplay>();
    [SerializeField] int padding = 30;
    Queue<List<PhraseSO>> PopulatedPhrases2 = new Queue<List<PhraseSO>>();
    private void Start() 
    {
        // currentPhraseDisplay = Instantiate(phraseDisplayPrefab, this.transform);
        // currentPhraseDisplay.Initialize(phraseSO);
        // PopulatePhrases(6);
        gameObject.SetActive(false);
    }
    private void OnEnable() 
    {
        PopulatePhrases(6);
    }
    private void OnDisable() 
    {
        GameManager.Instance.FinishFishing();
        gameObject.SetActive(false);
    }
    List<int> RandomList(int number)
    {
        List<int> list = new List<int>();
        while(number > 0)
        {
            int randomNum = Random.Range(1, System.Math.Min(3, number) + 1);
            list.Add(randomNum);
            number -= randomNum;
        }
        return list;

    }
    void ResetPhases()
    {
        foreach (var item in PhraseDisplays)
        {
            Destroy(item.gameObject);
        }
        PopulatedPhrases2 = new Queue<List<PhraseSO>>();
    }
    public void PopulatePhrases(int valueToPopulate = 3)
    {
        if(PhraseDisplays.Count != 0)
        {
            ResetPhases();
        }
        PhraseDisplays = new List<PhraseDisplay>();
        List<PhraseSO> tempLootTable = new List<PhraseSO>(LootTable);
        List<PhraseSO> tempRemovedLootTable = new List<PhraseSO>();
        List<int> randomList = RandomList(valueToPopulate);
        foreach (var number in randomList)
        {
            int valueToPopulateLeft = number;
            PopulatedPhrases = new List<PhraseSO>();
            while (valueToPopulateLeft > 0)
            {
                valueToPopulateLeft--;
                PhraseSO phraseSOtemp = tempLootTable[Random.Range(0, tempLootTable.Count)];
                PopulatedPhrases.Add(phraseSOtemp);
                tempLootTable.Remove(phraseSOtemp);
                (tempLootTable, tempRemovedLootTable) = RemoveSameFirstLettersFromList(tempLootTable, phraseSOtemp);
            }
            PopulatedPhrases2.Enqueue(PopulatedPhrases);
            tempLootTable.AddRange(tempRemovedLootTable);
        }
        DisplayPhrases(PopulatedPhrases2.Dequeue());
    }

    private void DisplayPhrases(List<PhraseSO> PhrasesToDisplay)
    {
        PhraseDisplays = new List<PhraseDisplay>();
        int AngleOffset = Random.Range(0, 360);
        for (int i = 0; i < PhrasesToDisplay.Count; i++)
        {
            currentPhraseDisplay = Instantiate(phraseDisplayPrefab, this.transform);
            currentPhraseDisplay.GetComponent<RectTransform>().anchoredPosition = returnPoint(i, PhrasesToDisplay.Count, AngleOffset);
            currentPhraseDisplay.Initialize(PhrasesToDisplay[i], this);
            PhraseDisplays.Add(currentPhraseDisplay);
        }
        currentPhraseDisplay = null;
    }

    Vector2 returnPoint(int index, int total, int offset)
    {
        int baseAngle = Random.Range(index*360/total + offset + padding, (index+1)*360/total + offset - padding);
        int range = Random.Range(200, 400);
        float angleInRadians = baseAngle * Mathf.Deg2Rad;
        float x = Mathf.Cos(angleInRadians);
        float y = Mathf.Sin(angleInRadians);
        Vector2 direction = new Vector2(x, y);
        return range*direction;
    }
    (List<PhraseSO>, List<PhraseSO>) RemoveSameFirstLettersFromList(List<PhraseSO> currentList, PhraseSO objectToRemove)
    {
        List<PhraseSO> newList = new List<PhraseSO>();
        List<PhraseSO> removedList = new List<PhraseSO>();
        char firstLetter = char.ToLower(objectToRemove.Value[0]);
        foreach (PhraseSO item in currentList)
        {
            if (firstLetter == char.ToLower(item.Value[0]))
            {
                removedList.Add(item);
            }
            else
            {
                newList.Add(item);
            }
        }
        return (newList, removedList);
    }

    private void Update()
    {
        CheckForPlayerInput();
    }

    private void CheckForPlayerInput()
    {
        if (Input.anyKeyDown)
        {
            for (char letter = 'a'; letter <= 'z'; letter++)
            {
                if (Input.GetKeyDown(letter.ToString()))
                {
                    CheckKey(letter);
                    return;
                }
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                CheckKey(' ');
                return;
            }
            if(Input.GetKeyDown(KeyCode.Backspace))
            {
                if(!currentPhraseDisplay) return;
                currentPhraseDisplay.ResetWord();
                currentPhraseDisplay = null;
            }
        }
    }
    private void SelectPhrase(char letter)
    {
        for (int i = 0; i < PhraseDisplays.Count ; i ++)
        {
            if (PhraseDisplays[i].PhraseSO.Value[0] == letter) 
            {
                currentPhraseDisplay = PhraseDisplays[i];
                return;
            }
        }
        currentPhraseDisplay = null;
    }
    private void CheckKey(char letter)
    {
        if(currentPhraseDisplay)
        {
            currentPhraseDisplay.CheckKey(letter);
        }
        else
        {
            SelectPhrase(letter);
            if(currentPhraseDisplay)
            {
                currentPhraseDisplay.CheckKey(letter);
            }
            else
            {
                Debug.Log("non displayed");
            }
        }
    }
    public void RemovePhrase(PhraseDisplay phrase)
    {
        PhraseDisplays.Remove(phrase);
        if(PhraseDisplays.Count <= 0)
        {
            if(PopulatedPhrases2.Count > 0)
            {
                DisplayPhrases(PopulatedPhrases2.Dequeue());
            }
            else
            {
                Debug.Log("you win");
                gameObject.SetActive(false);
            }
        }
    }
}
