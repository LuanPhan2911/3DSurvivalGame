using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotSingle : MonoBehaviour, IDropHandler
{

    [SerializeField] private bool isQuickSlot = false;
    public InventorySlotItem InventorySlotItem
    {
        get
        {
            if (transform.childCount == 0)
            {
                return null;
            }
            InventorySlotItem inventorySlotItem = transform.GetComponentInChildren<InventorySlotItem>();
            return inventorySlotItem;
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
        if (!DragDrop.itemBeingDragged.TryGetComponent(out InventorySlotItem dragInventorySlotItem))
        {
            return;
        }

        //if there is not item already then set our item.
        if (!InventorySlotItem)
        {
            if (!isQuickSlot)
            {
                dragInventorySlotItem.SetInventorySlotParent(this);

            }
            else
            {
                if (dragInventorySlotItem.GetInventoryItemSO().isEquippable)
                {
                    dragInventorySlotItem.SetInventorySlotParent(this);
                }
            }
        }
        else
        {
            // swap item if 2 item also equippable item
            if (InventorySlotItem.GetInventoryItemSO().isEquippable &&
            dragInventorySlotItem.GetInventoryItemSO().isEquippable)
            {
                InventorySlotSingle dragSlot = dragInventorySlotItem.GetInventorySlot();
                InventorySlotItem inventorySlotItem = InventorySlotItem;

                dragInventorySlotItem.SetInventorySlotParent(this);
                inventorySlotItem.SetInventorySlotParent(dragSlot);

            }
        }

    }
    public int GetSlotIndex()
    {
        if (!isQuickSlot)
        {
            return -1;
        }
        return GetComponent<QuickSlotNumber>().GetNumberIndex();
    }
    public bool IsQuickSlot()
    {
        return isQuickSlot;
    }





}
