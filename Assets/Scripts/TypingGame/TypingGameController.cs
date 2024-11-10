using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    List<PhraseDisplay> PhraseDisplays;
    [SerializeField] int padding = 30;
    private void Start() 
    {
        // currentPhraseDisplay = Instantiate(phraseDisplayPrefab, this.transform);
        // currentPhraseDisplay.Initialize(phraseSO);
        PopulatePhrases();
    }
    public void PopulatePhrases()
    {
        PhraseDisplays = new List<PhraseDisplay>();
        int valueToPopulate = 3;
        List<PhraseSO> tempLootTable = new List<PhraseSO>(LootTable);
        List<PhraseSO> tempRemovedLootTable = new List<PhraseSO>();
        while (valueToPopulate >0)
        {
            valueToPopulate --;
            PhraseSO phraseSOtemp = tempLootTable[Random.Range(0,tempLootTable.Count)];
            PopulatedPhrases.Add(phraseSOtemp);
            tempLootTable.Remove(phraseSOtemp);
            (tempLootTable,tempRemovedLootTable) =  RemoveSameFirstLettersFromList(tempLootTable, phraseSOtemp);
        }
        PhraseDisplays = new List<PhraseDisplay>();
        int AngleOffset = Random.Range(0, 360);
        for (int i = 0; i < PopulatedPhrases.Count ; i++)
        {
            currentPhraseDisplay = Instantiate(phraseDisplayPrefab, this.transform);
            currentPhraseDisplay.GetComponent<RectTransform>().anchoredPosition = returnPoint(i, PopulatedPhrases.Count, AngleOffset);
            currentPhraseDisplay.Initialize(PopulatedPhrases[i]);
            PhraseDisplays.Add(currentPhraseDisplay);
        }
        currentPhraseDisplay = null;
    }
    Vector2 returnPoint(int index, int total, int offset)
    {
        int baseAngle = Random.Range(index*360/total + offset + padding, (index+1)*360/total + offset - padding);
        int range = Random.Range(100, 300);
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
        for (int i = 0; i < PopulatedPhrases.Count ; i ++)
        {
            if (PopulatedPhrases[i].Value[0] == letter) 
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
}
