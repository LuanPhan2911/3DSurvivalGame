using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotContainer : MonoBehaviour
{
    [SerializeField] private List<InventorySlotSingle> inventorySlotList;

    private bool isInventoryFull = false;


    public List<InventorySlotSingle> GetInventorySlotList()
    {
        return inventorySlotList;
    }

    public bool TryGetAvailableSlot(InventoryItemSO inventoryItemSO, InventorySystem.ItemColor itemColor, out InventorySlotSingle availableInventorySlot)
    {

        foreach (InventorySlotSingle inventorySlot in inventorySlotList)
        {
            if (inventorySlot.IsAvailable(inventoryItemSO, itemColor))
            {
                availableInventorySlot = inventorySlot;
                return true;
            }
        }
        // inventory is full;
        isInventoryFull = true;
        availableInventorySlot = null;
        return false;
    }

    public bool GetIsInventoryFull()
    {
        return isInventoryFull;
    }

    public InventorySlotSingle GetEmptyInventorySlot()
    {
        foreach (InventorySlotSingle inventorySlot in inventorySlotList)
        {
            if (!inventorySlot.InventorySlotItem)
            {
                return inventorySlot;
            }
        }
        return null;
    }


}
