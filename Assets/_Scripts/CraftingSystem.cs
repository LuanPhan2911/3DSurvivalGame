using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance { get; private set; }

    [SerializeField] public const float craftingTimerMax = 3f;


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
        Crafting,
        Completed,
        Canceled
    }
    public State state;

    private void Awake()
    {
        Instance = this;
        state = State.Idle;
    }
    // private void Update()
    // {
    //     switch (state)
    //     {
    //         case State.Idle:
    //             break;
    //         case State.Confirm:


    //             break;
    //         case State.Crafting:
    //             break;
    //         case State.Completed:
    //             break;
    //         case State.Canceled:
    //             break;
    //         default:
    //             break;
    //     }
    // }
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

        // add new craft item to inventory
        InventorySystem.Instance.AddToInventory(craftingItemSO.inventoryItemSO, amount);



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
            if (craftingRequiredItemSO.inventoryItemSO.Id == inventoryItemSOChange.Id)
            {
                return true;
            }
        }
        // not required item change
        return false;
    }
}
