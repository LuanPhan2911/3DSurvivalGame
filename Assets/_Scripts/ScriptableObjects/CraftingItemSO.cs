using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CraftingItemSO : ScriptableObject
{
    public InventoryItemSO inventoryItemSO;

    public List<CraftingRequiredItemSO> craftingRequiredItemSOList;

}
