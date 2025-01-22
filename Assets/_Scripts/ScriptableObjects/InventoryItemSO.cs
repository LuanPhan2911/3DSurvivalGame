using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class InventoryItemSO : ScriptableObject
{
    public string itemName;
    public Sprite inventorySprite;
    public string originatedFromObjectName;
}
