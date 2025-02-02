using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class InventoryItemSO : BaseScriptableObject
{

    public string itemName;
    public Sprite sprite;
    public int maxAmountInSlot = 1;

    public float weight;



}
