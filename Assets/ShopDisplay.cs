using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopDisplay : MonoBehaviour
{
    [SerializeField] Image SpriteImage;
    [SerializeField] Image BackgroundIMG;
    [SerializeField] TextMeshProUGUI ItemName;
    [SerializeField] TextMeshProUGUI ItemValue;
    public void Initialize(ShopItem shopItem)
    {
        SpriteImage.sprite = shopItem.Sprite;
        ItemName.text = shopItem.Name;
        ItemValue.text = "$" + shopItem.Cost;
    }
    public void Select()
    {
        BackgroundIMG.color = Color.green;
    }
    public void UnSelect()
    {
        BackgroundIMG.color = Color.white;
    }

}
