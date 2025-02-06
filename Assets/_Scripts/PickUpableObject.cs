using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpableObject : BaseInteractableObject
{
    protected override void GameInput_OnAttackAction(object sender, EventArgs eventArgs)
    {
        if (IsCanInteract())
        {
            int amount = GetAmountItemProvided();
            InventorySystem.Instance.AddToInventory(this, amount);
            AlertUI.Instance.Alert(
                $"Picked up x{amount} {originalObjectSO.inventoryItemSO.itemName}."
            );

            if (IsNeedDestroy())
            {
                Destroy(gameObject);
            }
        }
    }

    protected override int GetAmountItemProvided()
    {
        return originalObjectSO.maxAmountItemProvided;
    }
    public override bool IsCanInteract()
    {
        return base.IsCanInteract() && originalObjectSO.canPickedUp;
    }
    protected override bool IsNeedDestroy()
    {
        return originalObjectSO.canPickedUp;
    }

}
