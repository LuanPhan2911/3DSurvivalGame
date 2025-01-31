using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemImageContainer : MonoBehaviour
{
    [SerializeField] private Transform inventoryItemImageTransformPrefab;

    private void Start()
    {
        Hide();
    }
    public void UpdateItemImage(InventoryItemSO inventoryItemSO)
    {

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach (InventorySystem.ItemColor itemColor in Enum.GetValues(typeof(InventorySystem.ItemColor)))
        {
            if (CraftingSystem.Instance.GetAmountCraftByItemColor(itemColor) > 0)
            {
                Transform inventoryImageTransform = Instantiate(inventoryItemImageTransformPrefab, transform);
                InventoryItemImage inventoryItemImage = inventoryImageTransform.GetComponent<InventoryItemImage>();
                inventoryItemImage.SetCraftItem(inventoryItemSO.sprite, CraftingSystem.Instance.GetAmountCraftByItemColor(itemColor));
                inventoryItemImage.SetBackgroundColor((int)itemColor);
            }
        }

    }
    public void Show()
    {

        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
