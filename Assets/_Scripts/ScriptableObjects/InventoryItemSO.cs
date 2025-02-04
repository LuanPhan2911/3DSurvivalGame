using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class InventoryItemSO : ScriptableObject
{

    public string itemName;
    public string description;
    public Sprite sprite;
    public GameObject itemModel;
    public int maxAmountInSlot = 1;
    public float weight;

    public bool isConsumable;
    public bool isEquippable;
    public float hpProvide;
    public float foodProvide;
    public float waterProvide;



}
