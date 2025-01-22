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

    public bool TryGetAvailableSlot(out InventorySlotSingle availableInventorySlot)
    {

        foreach (InventorySlotSingle inventorySlot in inventorySlotList)
        {
            if (inventorySlot.IsAvailable())
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


}
