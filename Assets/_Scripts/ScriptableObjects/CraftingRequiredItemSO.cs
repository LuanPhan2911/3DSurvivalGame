using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CraftingRequiredItemSO : BaseScriptableObject
{
    public InventoryItemSO inventoryItemSO;
    public int quantity;
}
