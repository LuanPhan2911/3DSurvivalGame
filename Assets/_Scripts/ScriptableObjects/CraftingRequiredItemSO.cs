using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CraftingRequiredItemSO : ScriptableObject
{
    public InventoryItemSO inventoryItemSO;
    public int quantity;
}
