using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    [SerializeField] private InventorySlotContainer inventorySlotContainer;
    [SerializeField] private Transform inventorySlotItemTransformPrefab;
    [SerializeField] public Color[] backgroundColorArray;

    private List<InventorySlotItem> inventorySlotItemList;

    public static InventorySystem Instance { get; private set; }

    public class OnInventoryItemChangedEventArgs : EventArgs
    {
        public InventoryItemSO inventoryItemSO;
    }
    public event EventHandler<OnInventoryItemChangedEventArgs> OnInventoryItemChanged;

    public enum ItemColor
    {
        Purple,
        Yellow,
        Blue,
        White,
    }


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
                count += inventorySlotItem.GetAmountInSlot();
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
                count += inventorySlotItem.GetAmountInSlot();
            }
        }
        return count;
    }

    private void InstantiateInventorySlotItem(InventorySlotSingle inventorySlotSingle, InventoryItemSO inventoryItemSO, int amount, ItemColor itemColor)
    {
        Transform inventorySlotItemTransform = Instantiate(inventorySlotItemTransformPrefab, inventorySlotSingle.transform);

        InventorySlotItem inventorySlotItem = inventorySlotItemTransform.GetComponent<InventorySlotItem>();
        inventorySlotItem.SetInventoryItemAdded(
            inventoryItemSO, inventorySlotSingle, amount, itemColor);

        inventorySlotItemList.Add(inventorySlotItem);

    }

    private void TryAddToInventory(InventoryItemSO inventoryItemSO, int amount, ItemColor itemColor)
    {
        if (inventorySlotContainer.TryGetAvailableSlot(inventoryItemSO, itemColor, out InventorySlotSingle availableInventorySlot))
        {

            if (availableInventorySlot.GetRemainAvailableSlot(inventoryItemSO, itemColor) == inventoryItemSO.maxAmountInSlot)
            {

                InstantiateInventorySlotItem(availableInventorySlot, inventoryItemSO, amount, itemColor);

                OnInventoryItemChanged?.Invoke(this, new OnInventoryItemChangedEventArgs
                {
                    inventoryItemSO = inventoryItemSO
                });
            }
            else if (amount <= availableInventorySlot.GetRemainAvailableSlot(inventoryItemSO, itemColor))
            {

                // slot can contain more item
                availableInventorySlot.InventorySlotItem.AddAmountInSlot(amount);
                OnInventoryItemChanged?.Invoke(this, new OnInventoryItemChangedEventArgs
                {
                    inventoryItemSO = inventoryItemSO
                });
            }
            else
            {
                //slot can contain more item but not enough
                int maxAmountAdded = availableInventorySlot.GetRemainAvailableSlot(inventoryItemSO, itemColor);
                int remainAmount = amount - maxAmountAdded;
                availableInventorySlot.InventorySlotItem.AddAmountInSlot(maxAmountAdded);
                // try get new available slot
                if (inventorySlotContainer.TryGetAvailableSlot(inventoryItemSO, itemColor, out InventorySlotSingle newAvailableInventorySlot))
                {
                    InstantiateInventorySlotItem(newAvailableInventorySlot, inventoryItemSO, remainAmount, itemColor);
                }
                else
                {
                    // trigger inventory full
                    Debug.Log("Inventory full");
                }
                OnInventoryItemChanged?.Invoke(this, new OnInventoryItemChangedEventArgs
                {
                    inventoryItemSO = inventoryItemSO
                });
            }
        }
        else
        {
            // trigger inventory full
            Debug.Log("Inventory full");

        }

    }



    public void AddToInventory(InteractableObject interactableObject, int amount)
    {
        int maxAmountAdded = interactableObject.GetInventoryItemSO().maxAmountInSlot;

        while (amount > 0)
        {
            // Xác định số lượng cần thêm ở lần lặp này
            int amountToAdd = Math.Min(amount, maxAmountAdded);

            // Gọi hàm TryAddToInventory với số lượng tính toán được
            TryAddToInventory(interactableObject.GetInventoryItemSO(), amount, ItemColor.White);

            // Giảm số lượng cần thêm còn lại
            amount -= amountToAdd;
        }

    }
    public void AddToInventory(InventoryItemSO inventoryItemSO, int amount, ItemColor itemColor)
    {
        int maxAmountAdded = inventoryItemSO.maxAmountInSlot;

        while (amount > 0)
        {
            // Xác định số lượng cần thêm ở lần lặp này
            int amountToAdd = Math.Min(amount, maxAmountAdded);

            // Gọi hàm TryAddToInventory với số lượng tính toán được
            TryAddToInventory(inventoryItemSO, amountToAdd, itemColor);

            // Giảm số lượng cần thêm còn lại
            amount -= amountToAdd;
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
    public void RemoveItemInInventory(InventoryItemSO inventoryItemSO, int amount)
    {

        int count = 0;
        for (int i = inventorySlotItemList.Count - 1; i > 0; i--)
        {
            InventorySlotItem inventorySlotItem = inventorySlotItemList[i];
            if (inventorySlotItem.GetInventoryItemSO().Id == inventoryItemSO.Id)
            {

                if (amount >= inventorySlotItem.GetAmountInSlot())
                {
                    inventorySlotItemList.RemoveAt(i);
                    Destroy(inventorySlotItem.gameObject);
                    count += inventorySlotItem.GetAmountInSlot();
                }
                else
                {
                    inventorySlotItem.SubAmountInSlot(amount);
                    count += amount;
                }

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
