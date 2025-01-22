using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    [SerializeField] private InventorySlotContainer inventorySlotContainer;
    [SerializeField] private Transform inventorySlotItemTransformPrefab;

    private List<InventorySlotItem> inventorySlotItemList;

    public static InventorySystem Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
        inventorySlotItemList = new List<InventorySlotItem>();
    }



    public void AddToInventory(InteractableObject interactableObject)
    {
        if (inventorySlotContainer.GetIsInventoryFull())
        {
            Debug.Log("Inventory is full");
            return;
        }
        if (inventorySlotContainer.TryGetAvailableSlot(out InventorySlotSingle availableInventorySlot))
        {
            Transform inventorySlotItemTransform = Instantiate(inventorySlotItemTransformPrefab, availableInventorySlot.transform);


            InventorySlotItem inventorySlotItem = inventorySlotItemTransform.GetComponent<InventorySlotItem>();
            inventorySlotItem.SetInventoryItemAdded(
                interactableObject.GetInventoryItemSO(), availableInventorySlot);
            Destroy(interactableObject.gameObject);

            inventorySlotItemList.Add(inventorySlotItem);

        }
    }

    public void DropItemFromInventory(InventorySlotItem dropInventoryItem)
    {

        foreach (InventorySlotItem inventorySlotItem in inventorySlotItemList)
        {
            if (inventorySlotItem == dropInventoryItem)
            {
                inventorySlotItemList.Remove(inventorySlotItem);
                break;
            }
        }
        Destroy(dropInventoryItem.gameObject);
    }
}
