using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotContainer : MonoBehaviour
{
    [SerializeField] private List<InventorySlotSingle> quickSlotItemList;

    private void Start()
    {
        GameInput.Instance.OnQuickSlotAction += GameInput_QuickSlotAction;
    }
    private void OnDestroy()
    {
        GameInput.Instance.OnQuickSlotAction -= GameInput_QuickSlotAction;
    }
    private void GameInput_QuickSlotAction(object sender, GameInput.OnQuickSlotActionEventArgs args)
    {


        InventorySlotSingle quickSlot = GetQuickSlotBySlotIndex(args.slotIndex);
        if (!quickSlot || !quickSlot.InventorySlotItem)
        {
            return;
        }
        if (quickSlot.InventorySlotItem.GetInventoryItemSO().isConsumable)
        {
            quickSlot.InventorySlotItem.ConsumeItem();
        }
        else
        {
            InventorySystem.Instance.SetSelectedQuickSlot(quickSlot);
        }


    }
    private InventorySlotSingle GetQuickSlotBySlotIndex(int slotIndex)
    {
        foreach (InventorySlotSingle quickSlot in quickSlotItemList)
        {
            QuickSlotNumber quickSlotNumber = quickSlot.GetComponentInChildren<QuickSlotNumber>();
            if (!quickSlotNumber)
            {
                return null;
            }
            if (quickSlotNumber.GetNumberIndex() == slotIndex)
            {
                return quickSlot;
            }
        }
        return null;
    }

    public InventorySlotSingle GetEmptyQuickSlot()
    {
        foreach (InventorySlotSingle quickSlot in quickSlotItemList)
        {
            if (!quickSlot.InventorySlotItem)
            {
                return quickSlot;
            }
        }
        return null;
    }
}
