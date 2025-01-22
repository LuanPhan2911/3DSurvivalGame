using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RequiredItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemText;



    public void SetRequiredItemText(CraftingRequiredItemSO craftingRequiredItemSO, int currentQuantity)
    {
        itemText.text = $"x{craftingRequiredItemSO.quantity} {craftingRequiredItemSO.inventoryItemSO.itemName} ({currentQuantity}/{craftingRequiredItemSO.quantity})";

        if (currentQuantity < craftingRequiredItemSO.quantity)
        {
            itemText.color = Color.red;
        }
        else
        {
            itemText.color = Color.green;
        }

    }
}
