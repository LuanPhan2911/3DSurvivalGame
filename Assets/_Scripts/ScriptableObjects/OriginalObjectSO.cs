using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class OriginalObjectSO : ScriptableObject
{
    public string objectName;
    public int maxHP;
    public int maxAmountItemProvided;
    public bool canPickedUp = false;
    public InventoryItemSO inventoryItemSO;


}
