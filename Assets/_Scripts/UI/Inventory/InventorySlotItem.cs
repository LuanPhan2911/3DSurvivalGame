using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotItem : MonoBehaviour
{
    private InventoryItemSO inventoryItemSO;
    private InventorySlotSingle inventorySlot;
    [SerializeField] private Image itemImage;


    public void SetInventoryItemAdded(
     InventoryItemSO inventoryItemSO, InventorySlotSingle inventorySlot)
    {

        this.inventoryItemSO = inventoryItemSO;
        this.inventorySlot = inventorySlot;

        itemImage.sprite = inventoryItemSO.sprite;
    }
    public InventoryItemSO GetInventoryItemSO()
    {
        return inventoryItemSO;
    }
    public InventorySlotSingle GetInventorySlot()
    {
        return inventorySlot;
    }
}
