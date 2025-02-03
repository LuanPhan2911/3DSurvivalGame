using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InventoryDropItem : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image image;
    public void OnDrop(PointerEventData eventData)
    {
        if (DragDrop.itemBeingDragged.TryGetComponent(out InventorySlotItem inventorySlotItem))
        {

            string message = $"Drop {inventorySlotItem.GetInventoryItemSO().itemName} x{inventorySlotItem.GetAmountInSlot()}?";
            ConfirmUI.Instance.ShowConfirm(
                message, () =>
                {
                    InventorySystem.Instance.RemoveItemInInventory(inventorySlotItem);
                }
            );
            image.color = Color.red;

        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (DragDrop.itemBeingDragged)
        {
            image.color = Color.green;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (DragDrop.itemBeingDragged)
        {
            image.color = Color.red;
        }
    }
}
