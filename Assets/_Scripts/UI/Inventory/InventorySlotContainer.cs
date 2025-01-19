using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotContainer : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> inventorySlotList;

    private bool isInventoryFull = false;


    public List<InventorySlot> GetInventorySlotList()
    {
        return inventorySlotList;
    }

    public bool TryGetAvailableSlot(out InventorySlot availableInventorySlot)
    {

        foreach (InventorySlot inventorySlot in inventorySlotList)
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
