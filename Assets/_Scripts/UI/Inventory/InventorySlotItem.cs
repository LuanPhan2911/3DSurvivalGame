using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotItem : MonoBehaviour
{
    private InteractableObjectSO interactableObjectSO;
    private InventorySlot inventorySlot;


    public void SetInteractableObjectAdded(
     InteractableObjectSO interactableObjectSO, InventorySlot inventorySlot)
    {

        this.interactableObjectSO = interactableObjectSO;
        this.inventorySlot = inventorySlot;
    }
    public InteractableObjectSO GetInteractableObjectSO()
    {
        return interactableObjectSO;
    }
    public InventorySlot GetInventorySlot()
    {
        return inventorySlot;
    }
}
