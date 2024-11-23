using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PhraseDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text FinishedWord;
    [SerializeField] TMP_Text WrongWord;
    [SerializeField] TMP_Text Word;
    [SerializeField] PhraseSO phraseSO;
    [SerializeField] Image ProgressBarSprite;
    public PhraseSO PhraseSO { get { return phraseSO; } }
    bool stateCorrectLetter;
    TypingGameController typingGameController;
    int index = 0;
    const string WRONGLETTERCOLOR = "<color=\"red\">";
    const string CORRECTLETTERCOLOR = "<color=\"grey\">";
    const string OGLETTERCOLOR = "<color=\"white\">";
    int Ticks = 150;
    int currentTicks = 150;

    public void Initialize(PhraseSO phraseSO, TypingGameController typingGameController)
    {
        this.phraseSO = phraseSO;
        this.typingGameController = typingGameController;
        SetWord();
    }
    public void CheckKey(char value)
    {
        if(value == char.ToLower(phraseSO.Value[index]))
        {
            CorrectLetter();
        }
        else
        {
            WrongLetter();
        }
    }
    void SetWord()
    {
        if (phraseSO == null)
        {
            Debug.LogError("phrase is missing");
            return;
        }
        ResetWord();
        GetComponent<TextWidthSize>()?.UpdateWidth();
        stateCorrectLetter = true;
    }
    void WrongLetter()
    {
        Word.text = CORRECTLETTERCOLOR + phraseSO.Value.Substring(0, index) + 
                    WRONGLETTERCOLOR + phraseSO.Value[index] + 
                    OGLETTERCOLOR + phraseSO.Value.Substring(index+1);
    }
    void CorrectLetter()
    {
        index ++;
        if(index > phraseSO.Value.Length - 1)
        {
            CompleteWord();
        }
        Word.text = CORRECTLETTERCOLOR + phraseSO.Value.Substring(0, index) + 
                    OGLETTERCOLOR + phraseSO.Value.Substring(index);
    }
    void CompleteWord()
    {
        typingGameController.RemovePhrase(this);
        Destroy(gameObject);
    }
    public void ResetWord()
    {
        index = 0;
        FinishedWord.text = phraseSO.Value;
        WrongWord.text = phraseSO.Value;
        Word.text = phraseSO.Value;
    }
    public void Tick()
    {
        currentTicks --;
        ProgressBarSprite.fillAmount = currentTicks*1f/Ticks;
        if(currentTicks == 0)
        {
            Debug.Log("you lost the fish");
            typingGameController.PhraseLost();
        }
    }
}
