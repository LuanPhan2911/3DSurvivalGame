using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotSingle : MonoBehaviour, IDropHandler
{

    public InventorySlotItem InventorySlotItem
    {
        get
        {
            if (transform.childCount == 0)
            {
                return null;
            }
            if (transform.GetChild(0).TryGetComponent(out InventorySlotItem inventorySlotItem))
            {
                return inventorySlotItem;
            }

            return null;
        }
    }

    public bool IsAvailable(InventoryItemSO inventoryItemSO, InventorySystem.ItemColor itemColor)
    {
        if (InventorySlotItem == null)
        {
            return true;
        }
        if (InventorySlotItem.GetInventoryItemSO() == inventoryItemSO && InventorySlotItem.GetItemColor() == itemColor)
        {
            return InventorySlotItem.GetAmountInSlot() < InventorySlotItem.GetMaxAmountInSlot();

        }
        return false;




    }

    public int GetRemainAvailableSlot(InventoryItemSO inventoryItemSO, InventorySystem.ItemColor itemColor)
    {
        if (InventorySlotItem == null)
        {
            return inventoryItemSO.maxAmountInSlot;
        }
        if (InventorySlotItem.GetInventoryItemSO() == inventoryItemSO && InventorySlotItem.GetItemColor() == itemColor)
        {
            return InventorySlotItem.GetMaxAmountInSlot() - InventorySlotItem.GetAmountInSlot();
        }

        return 0;


    }
    public void OnDrop(PointerEventData eventData)
    {


        //if there is not item already then set our item.
        if (!InventorySlotItem)
        {

            DragDrop.itemBeingDragged.transform.SetParent(transform);
            DragDrop.itemBeingDragged.transform.localPosition = new Vector2(0, 0);

        }



    }




}
