using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    [SerializeField] private InventorySlotContainer inventorySlotContainer;
    [SerializeField] private Transform inventorySlotItemTransformPrefab;

    private List<InventorySlotItem> inventorySlotItemList;

    public static InventorySystem Instance { get; private set; }

    public class OnInventoryItemChangedEventArgs : EventArgs
    {
        public InventoryItemSO inventoryItemSO;
    }
    public event EventHandler<OnInventoryItemChangedEventArgs> OnInventoryItemChanged;


    private void Awake()
    {
        Instance = this;
        inventorySlotItemList = new List<InventorySlotItem>();
    }

    public bool IsAvailableItem(InventoryItemSO inventoryItemSO, int amount)
    {
        int count = 0;
        foreach (InventorySlotItem inventorySlotItem in inventorySlotItemList)
        {
            if (inventorySlotItem.GetInventoryItemSO().Id == inventoryItemSO.Id)
            {
                count++;
                if (count >= amount)
                {
                    return true;
                }
            }

        }
        return false;
    }
    public int GetQuantityOfInventoryItem(InventoryItemSO inventoryItemSO)
    {
        int count = 0;
        foreach (InventorySlotItem inventorySlotItem in inventorySlotItemList)
        {
            if (inventorySlotItem.GetInventoryItemSO().Id == inventoryItemSO.Id)
            {
                count++;
            }
        }
        return count;
    }

    private bool TryAddToInventory(InventoryItemSO inventoryItemSO)
    {
        if (inventorySlotContainer.TryGetAvailableSlot(out InventorySlotSingle availableInventorySlot))
        {
            Transform inventorySlotItemTransform = Instantiate(inventorySlotItemTransformPrefab, availableInventorySlot.transform);


            InventorySlotItem inventorySlotItem = inventorySlotItemTransform.GetComponent<InventorySlotItem>();
            inventorySlotItem.SetInventoryItemAdded(
                inventoryItemSO, availableInventorySlot);

            inventorySlotItemList.Add(inventorySlotItem);

            OnInventoryItemChanged?.Invoke(this, new OnInventoryItemChangedEventArgs
            {
                inventoryItemSO = inventoryItemSO
            });

            return true;
        }
        else
        {
            // trigger inventory full
            Debug.Log("Inventory full");
            return false;
        }

    }

    private void TryAddToInventory(InteractableObject interactableObject)
    {

        if (TryAddToInventory(interactableObject.GetInventoryItemSO()))
        {
            if (interactableObject.IsWhenDestroy())
            {
                Destroy(interactableObject.gameObject);
            }
        }

    }

    public void AddToInventory(InteractableObject interactableObject, int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            TryAddToInventory(interactableObject);
        }
    }
    public void AddToInventory(InventoryItemSO inventoryItemSO, int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            TryAddToInventory(inventoryItemSO);
        }
    }

    public void RemoveItemInInventory(InventorySlotItem dropInventoryItem)
    {

        foreach (InventorySlotItem inventorySlotItem in inventorySlotItemList)
        {
            if (inventorySlotItem == dropInventoryItem)
            {
                inventorySlotItemList.Remove(inventorySlotItem);
                OnInventoryItemChanged?.Invoke(this, new OnInventoryItemChangedEventArgs
                {
                    inventoryItemSO = dropInventoryItem.GetInventoryItemSO()
                });
                Destroy(dropInventoryItem.gameObject);
                return;
            }
        }

    }
    public void RemoveItemInInventory(InventoryItemSO inventoryItemSO, int amount = 1)
    {

        int count = 0;
        for (int i = inventorySlotItemList.Count - 1; i > 0; i--)
        {
            InventorySlotItem inventorySlotItem = inventorySlotItemList[i];
            if (inventorySlotItem.GetInventoryItemSO().Id == inventoryItemSO.Id)
            {
                inventorySlotItemList.RemoveAt(i);
                Destroy(inventorySlotItem.gameObject);
                count++;

                if (count >= amount)
                {
                    // remove enough;

                    OnInventoryItemChanged?.Invoke(this, new OnInventoryItemChangedEventArgs
                    {
                        inventoryItemSO = inventoryItemSO
                    });
                    return;
                }
            }

        }
    }
}
