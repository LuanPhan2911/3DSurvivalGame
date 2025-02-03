using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    [SerializeField] private InventorySlotContainer inventorySlotContainer;
    [SerializeField] private Transform inventorySlotItemTransformPrefab;
    [SerializeField] public Color[] backgroundColorArray;
    [SerializeField] public InventoryItemInfoUI inventoryItemInfoUI;

    private List<InventorySlotItem> inventorySlotItemList;

    public static InventorySystem Instance { get; private set; }

    private InventorySlotItem selectedInventorySlotItem;

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
    public void SetSelectedInventorySlotItem(InventorySlotItem inventorySlotItem)
    {
        if (selectedInventorySlotItem == inventorySlotItem)
        {
            selectedInventorySlotItem = null;
        }
        else
        {
            selectedInventorySlotItem?.HideSelectedGameObject();
            selectedInventorySlotItem = inventorySlotItem;
        }

    }
    public InventorySlotItem GetSelectedInventorySlotItem()
    {
        return selectedInventorySlotItem;
    }

    public bool IsAvailableItem(InventoryItemSO inventoryItemSO, int amount)
    {
        int count = 0;
        foreach (InventorySlotItem inventorySlotItem in inventorySlotItemList)
        {
            if (inventorySlotItem.GetInventoryItemSO() == inventoryItemSO)
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
            if (inventorySlotItem.GetInventoryItemSO() == inventoryItemSO)
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

            }
            else if (amount <= availableInventorySlot.GetRemainAvailableSlot(inventoryItemSO, itemColor))
            {
                // slot can contain more item
                availableInventorySlot.InventorySlotItem.AddAmountInSlot(amount);
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

            }
            OnInventoryItemChanged?.Invoke(this, new OnInventoryItemChangedEventArgs
            {
                inventoryItemSO = inventoryItemSO
            });
            float weight = amount * inventoryItemSO.weight;
            PlayerStatus.Instance.SetWeight(weight);


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
            TryAddToInventory(interactableObject.GetInventoryItemSO(), amountToAdd, ItemColor.White);

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
    public float GetInventoryWeight()
    {
        float totalWeight = 0f;
        foreach (InventorySlotItem inventorySlotItem in inventorySlotItemList)
        {
            totalWeight += inventorySlotItem.GetInventoryItemWeight();
        }
        return totalWeight;
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
                float weight = inventorySlotItem.GetInventoryItemWeight();
                PlayerStatus.Instance.SetWeight(-1 * weight);
                if (selectedInventorySlotItem == dropInventoryItem)
                {
                    SetSelectedInventorySlotItem(null);
                }

                Destroy(dropInventoryItem.gameObject);
                return;
            }
        }


    }
    public void RemoveItemInInventory(InventorySlotItem dropInventoryItem, int amount, Action OnComplete = null)
    {

        int count = 0;
        foreach (InventorySlotItem inventorySlotItem in inventorySlotItemList)
        {
            if (inventorySlotItem == dropInventoryItem)
            {

                if (amount >= inventorySlotItem.GetAmountInSlot())
                {
                    inventorySlotItemList.Remove(inventorySlotItem);
                    count += inventorySlotItem.GetAmountInSlot();
                    inventorySlotItem.SubAllAmountInSlot();
                    if (selectedInventorySlotItem == dropInventoryItem)
                    {
                        SetSelectedInventorySlotItem(null);
                    }
                    Destroy(inventorySlotItem.gameObject);
                }
                else
                {
                    count += amount;
                    inventorySlotItem.SubAmountInSlot(amount);
                }

                if (count >= amount)
                {

                    // remove enough;

                    OnInventoryItemChanged?.Invoke(this, new OnInventoryItemChangedEventArgs
                    {
                        inventoryItemSO = inventorySlotItem.GetInventoryItemSO()
                    });

                    float weight = amount * inventorySlotItem.GetInventoryItemSO().weight;
                    PlayerStatus.Instance.SetWeight(-1 * weight);
                    if (inventorySlotItem.GetAmountInSlot() == 0)
                    {
                        OnComplete?.Invoke();

                    }

                    return;
                }
            }
        }


    }
    public void RemoveItemInInventory(InventoryItemSO inventoryItemSO, int amount)
    {

        int count = 0;
        for (int i = inventorySlotItemList.Count - 1; i > 0; i--)
        {
            InventorySlotItem inventorySlotItem = inventorySlotItemList[i];
            if (inventorySlotItem.GetInventoryItemSO() == inventoryItemSO)
            {

                if (amount >= inventorySlotItem.GetAmountInSlot())
                {
                    inventorySlotItemList.RemoveAt(i);
                    Destroy(inventorySlotItem.gameObject);
                    count += inventorySlotItem.GetAmountInSlot();
                    inventorySlotItem.SubAllAmountInSlot();
                }
                else
                {
                    count += amount;
                    inventorySlotItem.SubAmountInSlot(amount);
                }

                if (count >= amount)
                {

                    // remove enough;

                    OnInventoryItemChanged?.Invoke(this, new OnInventoryItemChangedEventArgs
                    {
                        inventoryItemSO = inventoryItemSO
                    });

                    float weight = amount * inventoryItemSO.weight;
                    PlayerStatus.Instance.SetWeight(-1 * weight);

                    return;
                }
            }

        }

    }
}
