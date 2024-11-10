using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextWidthSize : MonoBehaviour
{
    [SerializeField] TMP_Text textComponent;
    [SerializeField] RectTransform WordContainer;
    [SerializeField] float Padding = 10f;
    public void UpdateWidth()
    {
        float preferredWidth = textComponent.preferredWidth + Padding*2;
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, preferredWidth);
        WordContainer.GetComponent<RectTransform>().anchoredPosition -= new Vector2(Padding, 0);
    }
}
