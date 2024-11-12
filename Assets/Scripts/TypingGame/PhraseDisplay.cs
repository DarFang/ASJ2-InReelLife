using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class PhraseDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text FinishedWord;
    [SerializeField] TMP_Text WrongWord;
    [SerializeField] TMP_Text Word;
    [SerializeField] PhraseSO phraseSO;
    public PhraseSO PhraseSO { get { return phraseSO; } }
    bool stateCorrectLetter;
    TypingGameController typingGameController;

    public void Initialize(PhraseSO phraseSO, TypingGameController typingGameController)
    {
        this.phraseSO = phraseSO;
        this.typingGameController = typingGameController;
        SetWord();
    }
    public void CheckKey(char value)
    {
        if(value == char.ToLower(WrongWord.text[0]))
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
        if(stateCorrectLetter)
        {
            RemoveOGLetter();
            stateCorrectLetter = false;
        }
    }
    void CorrectLetter()
    {
        if (stateCorrectLetter)
        {
            RemoveOGLetter();
        }
        else
        {
            stateCorrectLetter = true;
        }
        RemoveRedLetter();
    }
    void RemoveRedLetter()
    {
        WrongWord.text = WrongWord.text.Substring(1);
        if(WrongWord.text.Length <= 0)
        {
            CompleteWord();
        }
    }
    void RemoveOGLetter()
    {
        Word.text = Word.text.Substring(1);
    }
    void CompleteWord()
    {
        typingGameController.RemovePhrase(this);
        Destroy(gameObject);
    }
    public void ResetWord()
    {
        FinishedWord.text = phraseSO.Value;
        WrongWord.text = phraseSO.Value;
        Word.text = phraseSO.Value;
    }
}
