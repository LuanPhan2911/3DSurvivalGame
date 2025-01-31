using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotItem : MonoBehaviour
{

    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private Image backgroundImage;

    private InventoryItemSO inventoryItemSO;
    private InventorySlotSingle inventorySlot;
    private int amountInSlot;
    private InventorySystem.ItemColor itemColor;



    public void SetInventoryItemAdded(
     InventoryItemSO inventoryItemSO, InventorySlotSingle inventorySlot, int amount, InventorySystem.ItemColor itemColor)
    {

        this.inventoryItemSO = inventoryItemSO;
        this.inventorySlot = inventorySlot;
        itemImage.sprite = inventoryItemSO.sprite;
        SetAmountInSlot(amount);
        SetBackgroundColor(itemColor);
    }
    private void SetBackgroundColor(InventorySystem.ItemColor itemColor)
    {
        this.itemColor = itemColor;
        backgroundImage.color = InventorySystem.Instance.backgroundColorArray[(int)itemColor];
    }

    private void SetAmountInSlot(int amount)
    {
        amountInSlot = amount;
        SetAmountText(amountInSlot);
    }
    public void AddAmountInSlot(int amount)
    {
        amountInSlot += amount;
        SetAmountText(amountInSlot);
    }
    public void SubAmountInSlot(int amount)
    {
        amountInSlot -= amount;
        SetAmountText(amountInSlot);
    }
    private void SetAmountText(int amount)
    {
        if (amount == 1)
        {
            amountText.gameObject.SetActive(false);
        }
        else
        {
            amountText.gameObject.SetActive(true);
            amountText.text = amountInSlot.ToString();
        }
    }
    public InventoryItemSO GetInventoryItemSO()
    {
        return inventoryItemSO;
    }
    public InventorySlotSingle GetInventorySlot()
    {
        return inventorySlot;
    }
    public int GetAmountInSlot()
    {
        return amountInSlot;
    }
    public InventorySystem.ItemColor GetItemColor()
    {
        return itemColor;
    }
    public int GetMaxAmountInSlot()
    {
        return inventoryItemSO.maxAmountInSlot;
    }
}
