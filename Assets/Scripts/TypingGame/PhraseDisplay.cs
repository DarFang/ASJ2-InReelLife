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
    bool stateCorrectLetter;
    public void Initialize(PhraseSO phraseSO)
    {
        this.phraseSO = phraseSO;
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
        FinishedWord.text = phraseSO.Value;
        WrongWord.text = phraseSO.Value;
        Word.text = phraseSO.Value;
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
        if(Word.text.Length <= 0)
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
        Destroy(gameObject);
    }
    public void ResetWord()
    {
        Word.text = FinishedWord.text;
        WrongWord.text = FinishedWord.text;
    }
}
