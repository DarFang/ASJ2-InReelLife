using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Image ItemImage;
    [SerializeField] Sprite DisableItem;
    bool isSelectable;
    public void UpdateDisplay(Sprite sprite)
    {
        image.color = Color.white;
        ItemImage.sprite = sprite;
        isSelectable = true;
        Color color = ItemImage.color;
        color.a = 1;
        ItemImage.color = color;
    }
    public void ClearDisplay()
    {
        image.color = Color.white;
        ItemImage.sprite = null;
        isSelectable = true;
        Color color = ItemImage.color;
        color.a = 0;
        ItemImage.color = color;
    }
    public void DisableDisplay()
    {
        image.color = Color.gray;
        ItemImage.sprite = DisableItem;
        isSelectable = false;
        Color color = ItemImage.color;
        color.a = 1;
        ItemImage.color = color;
    }
    public void Select()
    {
        image.color = Color.green;
    }
    public void UnSelect()
    {
        image.color = Color.white;
    }

}
