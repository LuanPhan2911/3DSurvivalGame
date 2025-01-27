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

    private void Start()
    {
        requiredItemTransformPrefab.gameObject.SetActive(false);
        InventorySystem.Instance.OnInventoryItemChanged += InventoryItemChanged;



        craftButton.onClick.AddListener(() =>
        {
            CraftingSystem.Instance.SetCraftingState(
                CraftingSystem.State.Confirm, craftingItemSO);

        });
        Hide();
    }
    private void OnDestroy()
    {
        InventorySystem.Instance.OnInventoryItemChanged -= InventoryItemChanged;
    }
    private void InventoryItemChanged(object sender, InventorySystem.OnInventoryItemChangedEventArgs args)
    {

        if (CraftingSystem.Instance.IsCraftingRequiredItemChange(craftingItemSO, args.inventoryItemSO))
        {
            UpdateRequiredItemText();
            UpdateCraftButton();
        }

    }

    private void UpdateCraftButton()
    {
        int minAmountCraftItem = 1;
        List<CraftingRequiredItemSO> craftingRequiredItemSOList = craftingItemSO.craftingRequiredItemSOList;
        if (CraftingSystem.Instance.IsEnoughRequiredItemToCraft(craftingRequiredItemSOList, minAmountCraftItem))
        {

            craftButton.gameObject.SetActive(true);
        }
        else
        {
            craftButton.gameObject.SetActive(false);
        }
    }


}
