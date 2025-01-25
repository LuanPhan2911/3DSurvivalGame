using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Craft(CraftingItemSO craftingItemSO)
    {
        // check can enough item;
        if (!IsEnoughRequiredItemToCraft(craftingItemSO.craftingRequiredItemSOList))
        {
            Debug.Log("Not enough to craft");
            return;
        }

        //destroy inventory item used
        foreach (CraftingRequiredItemSO craftingRequiredItemSO in craftingItemSO.craftingRequiredItemSOList)
        {
            InventorySystem.Instance.RemoveItemInInventory(craftingRequiredItemSO.inventoryItemSO, craftingRequiredItemSO.quantity);
        }

        // add new craft item to inventory
        InventorySystem.Instance.AddToInventory(craftingItemSO.inventoryItemSO);



    }

    public bool IsEnoughRequiredItemToCraft(List<CraftingRequiredItemSO> craftingRequiredItemSOList)
    {
        bool isEnough = true;
        foreach (CraftingRequiredItemSO craftingRequiredItemSO in craftingRequiredItemSOList)
        {
            if (!InventorySystem.Instance.IsAvailableItem(craftingRequiredItemSO.inventoryItemSO, craftingRequiredItemSO.quantity))
            {
                isEnough = false;
                break;
            }
        }
        return isEnough;
    }
}
