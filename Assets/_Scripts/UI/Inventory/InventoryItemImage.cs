using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemImage : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI amountText;


    [SerializeField] private Image backgroundImage;


    public void SetCraftItem(Sprite sprite, int amount)
    {

        image.sprite = sprite;
        amountText.text = amount.ToString();
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void SetBackgroundColor(int colorIndex)
    {
        backgroundImage.color = InventorySystem.Instance.backgroundColorArray[colorIndex];
    }
    public void SetInventoryItem(Sprite sprite, int amount, int colorIndex)
    {
        SetBackgroundColor(colorIndex);
        SetCraftItem(sprite, amount);
    }
}
