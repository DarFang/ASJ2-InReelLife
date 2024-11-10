using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingGameController : MonoBehaviour
{
    [SerializeReference] PhraseDisplay phraseDisplayPrefab;
    PhraseDisplay currentPhraseDisplay;
    [SerializeField] PhraseSO phraseSO;
    private void Start() 
    {
        currentPhraseDisplay = Instantiate(phraseDisplayPrefab, this.transform);
        currentPhraseDisplay.Initialize(phraseSO);
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
                    currentPhraseDisplay.CheckKey(letter);
                    return;
                }
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                currentPhraseDisplay.CheckKey(' ');
                return;
            }
            if(Input.GetKeyDown(KeyCode.Backspace))
            {
                currentPhraseDisplay.ResetWord();
            }
        }
    }
}
