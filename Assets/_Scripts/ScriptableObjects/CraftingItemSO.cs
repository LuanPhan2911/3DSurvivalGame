using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CraftingItemSO : ScriptableObject
{
    public string craftingItemName;
    public Sprite itemImage;

    public List<CraftingRequiredItemSO> craftingRequiredItemSOList;

}
