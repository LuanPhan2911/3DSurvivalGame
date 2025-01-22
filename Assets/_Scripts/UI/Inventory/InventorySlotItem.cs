using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotItem : MonoBehaviour
{
    private InventoryItemSO inventoryItemSO;
    private InventorySlotSingle inventorySlot;


    public void SetInventoryItemAdded(
     InventoryItemSO inventoryItemSO, InventorySlotSingle inventorySlot)
    {

        this.inventoryItemSO = inventoryItemSO;
        this.inventorySlot = inventorySlot;
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
