using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotItem : MonoBehaviour
{

    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI amountText;
    private int amountInSlot;
    private InventoryItemSO inventoryItemSO;
    private InventorySlotSingle inventorySlot;



    public void SetInventoryItemAdded(
     InventoryItemSO inventoryItemSO, InventorySlotSingle inventorySlot, int amount)
    {

        this.inventoryItemSO = inventoryItemSO;
        this.inventorySlot = inventorySlot;
        itemImage.sprite = inventoryItemSO.sprite;

        SetAmountInSlot(amount);
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
    public int GetMaxAmountInSlot()
    {
        return inventoryItemSO.maxAmountInSlot;
    }
}
