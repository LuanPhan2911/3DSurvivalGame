using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDropItem : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (DragDrop.itemBeingDragged.TryGetComponent(out InventorySlotItem inventorySlotItem))
        {
            InventorySystem.Instance.RemoveItemInInventory(inventorySlotItem);
            Debug.Log("Item dropped");
        }

    }
}
