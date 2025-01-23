using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class OriginalObjectSO : ScriptableObject
{
    public string objectName;
    public InventoryItemSO inventoryItemSO;
    public int maxHP;
    public int maxAmountItemProvided;


}
