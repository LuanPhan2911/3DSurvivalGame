using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private InventoryItemImage inventoryItemImage;

    [SerializeField] private Button dropButton;
    [SerializeField] private Button useButton;

    private InventorySlotItem inventorySlotItem;


    private void Start()
    {
        dropButton.onClick.AddListener(() =>
        {
            string message = $"Drop {inventorySlotItem.GetInventoryItemSO().itemName} x{inventorySlotItem.GetAmountInSlot()}?";
            ConfirmUI.Instance.ShowConfirm(
                message, () =>
                {
                    InventorySystem.Instance.RemoveItemInInventory(inventorySlotItem);

                }
            );
        });
        useButton.onClick.AddListener(() =>
        {
            InventoryItemSO inventoryItemSO = inventorySlotItem.GetInventoryItemSO();
            // can consumable

            inventorySlotItem.ConsumeItem();

            // can quick equip
            if (!inventoryItemSO.isConsumable)
            {
                InventorySystem.Instance.SetEquippableItemToEmptyQuickSlot(inventorySlotItem);
                Hide();
            }
        });
        InventorySystem.Instance.OnInventoryItemChanged += InventorySystem_OnItemChanged;

    }
    private void OnDestroy()
    {
        InventorySystem.Instance.OnInventoryItemChanged -= InventorySystem_OnItemChanged;
    }
    private void InventorySystem_OnItemChanged(object sender, InventorySystem.OnInventoryItemChangedEventArgs args)
    {
        if (!InventorySystem.Instance.GetSelectedInventorySlotItem())
        {
            return;
        }
        if (InventorySystem.Instance.GetSelectedInventorySlotItem() == inventorySlotItem)
        {
            SetInventoryItemInfo(inventorySlotItem);
        }
    }
    public void SetInventoryItemInfo(InventorySlotItem inventorySlotItem)
    {
        Show();
        this.inventorySlotItem = inventorySlotItem;
        InventoryItemSO inventoryItemSO = inventorySlotItem.GetInventoryItemSO();

        itemNameText.text = $"{inventoryItemSO.itemName} x{inventorySlotItem.GetAmountInSlot()}";
        itemNameText.color = InventorySystem.Instance.backgroundColorArray[(int)inventorySlotItem.GetItemColor()];
        itemDescriptionText.text = inventoryItemSO.description;
        inventoryItemImage.SetInventoryItem(inventoryItemSO.sprite,
            inventorySlotItem.GetAmountInSlot(), (int)inventorySlotItem.GetItemColor());



    }



    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
