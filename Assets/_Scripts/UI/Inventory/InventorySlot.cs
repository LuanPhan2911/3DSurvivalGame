using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
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

    public bool IsAvailable()
    {
        return InventorySlotItem == null;
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
