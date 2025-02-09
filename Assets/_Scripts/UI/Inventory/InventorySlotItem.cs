using System;
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
    [SerializeField] private DragDrop dragDrop;


    private InventoryItemSO inventoryItemSO;
    private InventorySlotSingle inventorySlot;
    private int amountInSlot;
    private InventorySystem.ItemColor itemColor;



    private void Start()
    {
        selectedGameObject.SetActive(false);
        dragDrop.OnDragStart += DragDrop_OnItemDragStart;
    }
    private void OnDestroy()
    {
        dragDrop.OnDragStart -= DragDrop_OnItemDragStart;
    }


    private void DragDrop_OnItemDragStart(object sender, EventArgs args)
    {
        InventorySystem.Instance.SetSelectedInventorySlotItem(null);
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
        InventorySlotSingle inventorySlotSingle = GetComponentInParent<InventorySlotSingle>();
        if (inventorySlotSingle.IsQuickSlot())
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                InventorySystem.Instance.SetItemFromQuickSlotToEmptyInventorySlot(this);
            }
            return;
        }

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // left click
            InventorySystem.Instance.SetSelectedInventorySlotItem(this);
            InventorySystem.Instance.inventoryItemInfoUI.SetInventoryItemInfo(this);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            // right click

            //consume or equipment item
            ConsumeItem();
            if (!inventoryItemSO.isConsumable)
            {
                InventorySystem.Instance.SetEquippableItemToEmptyQuickSlot(this);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventorySlotSingle inventorySlotSingle = GetComponentInParent<InventorySlotSingle>();
        if (inventorySlotSingle.IsQuickSlot())
        {
            return;
        }

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
        if (!TryGetFoodInventoryItemSO(out FoodInventoryItemSO foodInventoryItemSO))
        {
            Debug.Log("Inventory item cannot consume");
            return;
        }
        PlayerStatus.Instance.SetHp(foodInventoryItemSO.hpProvide);
        PlayerStatus.Instance.SetFood(foodInventoryItemSO.foodProvide);
        PlayerStatus.Instance.SetWater(foodInventoryItemSO.waterProvide);


        InventorySystem.Instance.RemoveItemInInventory(this, 1, () =>
        {
            InventorySystem.Instance.inventoryItemInfoUI.Hide();
        });

    }
    public bool TryGetFoodInventoryItemSO(out FoodInventoryItemSO foodInventoryItemSO)
    {
        if (!inventoryItemSO || !inventoryItemSO.isConsumable)
        {
            foodInventoryItemSO = null;
            return false;
        }
        foodInventoryItemSO = inventoryItemSO as FoodInventoryItemSO;
        return true;

    }
    public bool TryGetEquippableInventoryItemSO(out EquippableInventoryItemSO equippableInventoryItemSO)
    {
        if (!inventoryItemSO || !inventoryItemSO.isEquippable)
        {
            equippableInventoryItemSO = null;
            return false;
        }
        equippableInventoryItemSO = inventoryItemSO as EquippableInventoryItemSO;
        return true;
    }
    public void SetInventorySlot(InventorySlotSingle inventorySlot)
    {
        this.inventorySlot = inventorySlot;
    }
    public void SetInventorySlotParent(InventorySlotSingle inventorySlot)
    {
        this.inventorySlot = inventorySlot;
        transform.SetParent(inventorySlot.transform);
        transform.localPosition = Vector3.zero;
    }
}
