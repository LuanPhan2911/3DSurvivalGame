using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class OriginalObjectSO : BaseScriptableObject
{
    public string objectName;
    public InventoryItemSO inventoryItemSO;
    public int maxHP;
    public int maxAmountItemProvided;
    public bool canPickedUp = false;


}
