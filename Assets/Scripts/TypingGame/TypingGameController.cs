using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

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
    public UnityEvent OnFishWin = new UnityEvent();
    public UnityEvent OnFishLose = new UnityEvent();
    private void Start() 
    {
        // currentPhraseDisplay = Instantiate(phraseDisplayPrefab, this.transform);
        // currentPhraseDisplay.Initialize(phraseSO);
        // PopulatePhrases(6);
        LootTable = ParseCSV("Phrase Bank.csv");
        gameObject.SetActive(false);
        AddListeners();
    }
    private void OnEnable() 
    {
        PopulatePhrases(6);
        StartCoroutine(TickDisplay());
    }
    public void AddListeners()
    {
        GameManager.Instance.AddListener();
    }
    public void RemoveListeners()
    {
        GameManager.Instance.RemoveListener();
    }
    private void OnDisable() 
    {
        GameManager.Instance.FinishFishing();
        gameObject.SetActive(false);
        StopAllCoroutines();
        RemoveListeners();
    }
    List<int> RandomList(int number)
    {
        List<int> list = new List<int>();
        while(number > 0)
        {
            int randomNum = UnityEngine.Random.Range(1, System.Math.Min(3, number) + 1);
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
                PhraseSO phraseSOtemp = tempLootTable[UnityEngine.Random.Range(0, tempLootTable.Count)];
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
        int AngleOffset = UnityEngine.Random.Range(0, 360);
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
        int baseAngle = UnityEngine.Random.Range(index*360/total + offset + padding, (index+1)*360/total + offset - padding);
        int range = UnityEngine.Random.Range(200, 400);
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
    IEnumerator TickDisplay()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.05f);
            if(PhraseDisplays.Count>0)
            {
                foreach (var item in PhraseDisplays)
                {
                    item.Tick();
                }
            }
        }
        
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
            if(Input.GetKeyDown(KeyCode.Period))
            {
                CheckKey('.');
                return;
            }
            if(Input.GetKeyDown(KeyCode.Quote))
            {
                CheckKey('\'');
                return;
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                CheckKey(' ');
                return;
            }
            if(Input.GetKeyDown(KeyCode.Exclaim))
            {
                CheckKey('!');
                return;
            }
            if(Input.GetKeyDown(KeyCode.DoubleQuote))
            {
                CheckKey('\"');
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
            if (char.ToLower(PhraseDisplays[i].PhraseSO.Value[0]) == letter) 
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
                OnFishWin?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
    private List<PhraseSO> ParseCSV(string fileName)
    {
        List<PhraseSO> results = new List<PhraseSO>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (!File.Exists(filePath))
        {
            Debug.LogError($"CSV file not found at path: {filePath}");
            return results;
        }

        var lines = File.ReadAllLines(filePath);

        for (int i = 1; i < lines.Length; i++)
        {
            var values = lines[i].Split(',');

            PhraseSO phrase = new PhraseSO
            {
                Value = values[1],
                Tier = values[2],
                Event = values[3],
                Location = values[4]
            };
            if(phrase.Tier == "Tier0")
            {
                results.Add(phrase);
            }
        }

        return results;
    }

    public void PhraseLost()
    {
        gameObject.SetActive(false);
        OnFishLose?.Invoke();
    }
}

