using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
public class Dialog : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComponent;
    public string Name;
    public string[] lines;
    public float textSpeed;
    private int index;
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialog();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }
    void StartDialog()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        string richTextPattern = @"<.*>";
        if (Regex.IsMatch(lines[index], richTextPattern))
        {
            MatchCollection matches = Regex.Matches(lines[index], richTextPattern);
            int textIndex = 0;
            int tagIndex = 0;
            while (textIndex < lines[index].Length)
            {
                if(tagIndex < matches.Count && matches[tagIndex].Index == textIndex)
                {
                    textComponent.text += matches[tagIndex].Value;
                    textIndex += matches[tagIndex].Length;
                    tagIndex ++;
                }
                else
                {
                    textComponent.text += lines[index][textIndex];
                    textIndex ++;
                    yield return new WaitForSeconds(textSpeed);
                }
            }
        }
        else
        {
            foreach (char c in lines[index])
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }
    }
    void NextLine()
    {
        if(index <lines.Length -1)
        {
            index ++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
