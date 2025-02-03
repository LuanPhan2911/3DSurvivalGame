using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private GameObject selectedGameObject;


    private InventoryItemSO inventoryItemSO;
    private InventorySlotSingle inventorySlot;
    private int amountInSlot;
    private InventorySystem.ItemColor itemColor;


    private void Start()
    {
        selectedGameObject.SetActive(false);
    }
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
    public void SubAllAmountInSlot()
    {
        amountInSlot = 0;
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

    public float GetInventoryItemWeight()
    {
        return inventoryItemSO.weight * amountInSlot;
    }


    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // left click
            InventorySystem.Instance.SetSelectedInventorySlotItem(this);
            InventorySystem.Instance.inventoryItemInfoUI.SetInventoryItemInfo(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selectedGameObject.SetActive(true);
        InventorySlotItem selectedSlotItem = InventorySystem.Instance.GetSelectedInventorySlotItem();
        if (!selectedSlotItem)
        {
            InventorySystem.Instance.inventoryItemInfoUI.SetInventoryItemInfo(this);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventorySlotItem selectedSlotItem = InventorySystem.Instance.GetSelectedInventorySlotItem();

        if (!selectedSlotItem)
        {
            selectedGameObject.SetActive(false);
            InventorySystem.Instance.inventoryItemInfoUI.Hide();
        }
        else
        {
            if (selectedSlotItem != this)
            {
                selectedGameObject.SetActive(false);
            }
        }


    }
    public void HideSelectedGameObject()
    {
        selectedGameObject.SetActive(false);
    }

    public void ConsumeItem()
    {
        if (!inventoryItemSO.isConsumable)
        {
            Debug.Log("Inventory item cannot consume");
            return;
        }
        PlayerStatus.Instance.SetHp(inventoryItemSO.hpProvide);
        PlayerStatus.Instance.SetFood(inventoryItemSO.foodProvide);
        PlayerStatus.Instance.SetWater(inventoryItemSO.waterProvide);


        InventorySystem.Instance.RemoveItemInInventory(this, 1, () =>
        {
            InventorySystem.Instance.inventoryItemInfoUI.Hide();
        });

    }
}
