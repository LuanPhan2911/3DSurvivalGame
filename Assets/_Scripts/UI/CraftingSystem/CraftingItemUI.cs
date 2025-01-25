using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingItemUI : BaseUI
{

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Image itemImage;
    [SerializeField] private Transform requiredItemTransformPrefab;
    [SerializeField] private Transform requiredItemContainerTransform;
    [SerializeField] private Button craftButton;

    private CraftingItemSO craftingItemSO;

    public void SetCraftingItemSO(CraftingItemSO craftingItemSO)
    {
        this.craftingItemSO = craftingItemSO;

        titleText.text = craftingItemSO.inventoryItemSO.itemName;
        itemImage.sprite = craftingItemSO.inventoryItemSO.sprite;

        UpdateRequiredItemText();
        UpdateCraftButton();
    }
    private void UpdateRequiredItemText()
    {
        foreach (Transform child in requiredItemContainerTransform)
        {
            if (child == requiredItemTransformPrefab)
            {
                continue;
            }
            else
            {
                Destroy(child.gameObject);
            }
        }
        foreach (CraftingRequiredItemSO craftingRequiredItemSO in craftingItemSO.craftingRequiredItemSOList)
        {
            Transform requiredItemTransform = Instantiate(requiredItemTransformPrefab, requiredItemContainerTransform);

            int quantity = InventorySystem.Instance.GetQuantityOfInventoryItem(craftingRequiredItemSO.inventoryItemSO);
            requiredItemTransform.GetComponent<RequiredItem>().SetRequiredItemText(craftingRequiredItemSO, quantity);
            requiredItemTransform.gameObject.SetActive(true);
        }
    }
    private void UpdateCraftButton()
    {
        if (CraftingSystem.Instance.IsEnoughRequiredItemToCraft(craftingItemSO.craftingRequiredItemSOList))
        {
            craftButton.enabled = true;
        }
        else
        {
            craftButton.enabled = false;
        }
    }

    private void Start()
    {
        requiredItemTransformPrefab.gameObject.SetActive(false);
        InventorySystem.Instance.OnInventoryItemChanged += InventoryItemChanged;

        craftButton.onClick.AddListener(() =>
        {
            CraftingSystem.Instance.Craft(craftingItemSO);
            Debug.Log("Craft");
        });
        Hide();
    }
    private void OnDestroy()
    {
        InventorySystem.Instance.OnInventoryItemChanged -= InventoryItemChanged;
    }
    private void InventoryItemChanged(object sender, InventorySystem.OnInventoryItemChangedEventArgs args)
    {

        if (IsCraftingRequiredItemChange(args.inventoryItemSO))
        {
            UpdateRequiredItemText();
            UpdateCraftButton();
        }

    }

    private bool IsCraftingRequiredItemChange(InventoryItemSO inventoryItemSO)
    {
        if (craftingItemSO == null)
        {
            // not set crafting item so
            return false;
        }
        foreach (CraftingRequiredItemSO craftingRequiredItemSO in craftingItemSO.craftingRequiredItemSOList)
        {
            if (craftingRequiredItemSO.inventoryItemSO.Id == inventoryItemSO.Id)
            {
                return true;
            }
        }
        // not required item change
        return false;
    }
}
