using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance { get; private set; }

    public const float craftingTimerMax = 1f;


    public class OnCraftingStateChangedEventArgs : EventArgs
    {
        public State state;
        public CraftingItemSO craftingItemSO;

    };

    public event EventHandler<OnCraftingStateChangedEventArgs> OnCraftingStateChanged;
    public enum State
    {
        Idle,
        Confirm,
        ManualConfirm,
        AutoCrafting,
        ManualCrafting,
        Completed,
        Canceled
    }


    private Dictionary<InventorySystem.ItemColor, float> rateColorDict;
    private Dictionary<InventorySystem.ItemColor, int> amountColorDict;
    public State state;

    private void Awake()
    {
        Instance = this;
        rateColorDict = new Dictionary<InventorySystem.ItemColor, float>();
        amountColorDict = new Dictionary<InventorySystem.ItemColor, int>();
        state = State.Idle;
        ResetRateColorDict();

    }


    private void ResetRateColorDict()
    {
        rateColorDict[InventorySystem.ItemColor.Purple] = 0.05f;
        rateColorDict[InventorySystem.ItemColor.Yellow] = 0.1f;
        rateColorDict[InventorySystem.ItemColor.Blue] = 0.75f;
        rateColorDict[InventorySystem.ItemColor.White] = 0.1f;

    }
    private void ResetAmountColorDict()
    {
        foreach (InventorySystem.ItemColor itemColor in Enum.GetValues(typeof(InventorySystem.ItemColor)))
        {
            amountColorDict[itemColor] = 0;
        }
    }
    public int GetAmountCraftByItemColor(InventorySystem.ItemColor itemColor)
    {
        return amountColorDict[itemColor];
    }
    public void SetRateColorDict(float purpleRate, float yellowRate, float blueRate, float whiteRate)
    {

        rateColorDict[InventorySystem.ItemColor.Purple] = purpleRate;
        rateColorDict[InventorySystem.ItemColor.Yellow] = yellowRate;
        rateColorDict[InventorySystem.ItemColor.Blue] = blueRate;
        rateColorDict[InventorySystem.ItemColor.White] = whiteRate;
    }

    public void SetCraftingState(State state, CraftingItemSO craftingItemSO)
    {
        this.state = state;
        OnCraftingStateChanged?.Invoke(this, new OnCraftingStateChangedEventArgs
        {
            state = state,
            craftingItemSO = craftingItemSO,

        });
    }

    public void Craft(CraftingItemSO craftingItemSO, int amount)
    {
        // check can enough item;
        // checked in Crafting Action

        //destroy inventory item used
        foreach (CraftingRequiredItemSO craftingRequiredItemSO in craftingItemSO.craftingRequiredItemSOList)
        {
            InventorySystem.Instance.RemoveItemInInventory(craftingRequiredItemSO.inventoryItemSO, craftingRequiredItemSO.quantity * amount);
        }

        //update inventory item color
        UpdateAmountCraftItem(amount);
        // add new craft item to inventory

        foreach (KeyValuePair<InventorySystem.ItemColor, int> entry in amountColorDict)
        {
            InventorySystem.ItemColor itemColor = entry.Key;
            int amountItemColor = entry.Value;
            InventorySystem.Instance.AddToInventory(craftingItemSO.inventoryItemSO, amountItemColor, itemColor);
        }




        ResetRateColorDict();

    }
    private void UpdateAmountCraftItem(int amount)
    {
        ResetAmountColorDict();
        float totalRate = 0f;
        foreach (float rate in rateColorDict.Values)
        {
            totalRate += rate;
        }

        if (totalRate == 0) return; // Tránh chia cho 0

        int totalAssigned = 0; // Tổng số lượng đã gán


        // Tính toán số lượng ban đầu theo tỷ lệ
        foreach (var pair in rateColorDict)
        {
            int allocatedAmount = Mathf.FloorToInt(amount * (pair.Value / totalRate));
            amountColorDict[pair.Key] = allocatedAmount;
            totalAssigned += allocatedAmount;
        }

        // Phân phối số dư còn lại do làm tròn xuống
        int remaining = amount - totalAssigned;
        foreach (var pair in rateColorDict.OrderByDescending(p => p.Value))
        {
            if (remaining <= 0) break;
            amountColorDict[pair.Key]++;
            remaining--;
        }



    }

    public bool IsEnoughRequiredItemToCraft(List<CraftingRequiredItemSO> craftingRequiredItemSOList, int amount)
    {
        bool isEnough = true;
        foreach (CraftingRequiredItemSO craftingRequiredItemSO in craftingRequiredItemSOList)
        {
            if (!InventorySystem.Instance.IsAvailableItem(craftingRequiredItemSO.inventoryItemSO, craftingRequiredItemSO.quantity * amount))
            {
                isEnough = false;
                break;
            }
        }
        return isEnough;
    }
    public int GetMaxAmountCraftItem(List<CraftingRequiredItemSO> craftingRequiredItemSOList)
    {
        int maxAmount = int.MaxValue;
        foreach (CraftingRequiredItemSO craftingRequiredItemSO in craftingRequiredItemSOList)
        {
            int amount = InventorySystem.Instance.GetQuantityOfInventoryItem(craftingRequiredItemSO.inventoryItemSO) / craftingRequiredItemSO.quantity;
            maxAmount = Mathf.Min(maxAmount, amount);
        }
        return maxAmount;
    }
    public bool IsCraftingRequiredItemChange(CraftingItemSO craftingItemSO, InventoryItemSO inventoryItemSOChange)
    {
        if (craftingItemSO == null)
        {
            // not set crafting item so
            return false;
        }
        foreach (CraftingRequiredItemSO craftingRequiredItemSO in craftingItemSO.craftingRequiredItemSOList)
        {
            if (craftingRequiredItemSO.inventoryItemSO == inventoryItemSOChange)
            {
                return true;
            }
        }
        // not required item change
        return false;
    }
}
