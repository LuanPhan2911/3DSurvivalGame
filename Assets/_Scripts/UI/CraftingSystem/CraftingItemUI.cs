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

        titleText.text = craftingItemSO.craftingItemName;
        itemImage.sprite = craftingItemSO.itemImage;

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

            requiredItemTransform.GetComponent<RequiredItem>().SetRequiredItemText(craftingRequiredItemSO, 0);
            requiredItemTransform.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        requiredItemTransformPrefab.gameObject.SetActive(false);

        Hide();
    }
}
