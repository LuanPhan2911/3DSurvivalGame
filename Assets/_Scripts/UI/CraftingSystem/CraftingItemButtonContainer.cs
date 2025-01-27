using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingItemButtonContainer : MonoBehaviour
{
    [SerializeField] private List<CraftingItemButtonSingle> craftItemButtonSingleList;




    private void Start()
    {
        foreach (CraftingItemButtonSingle craftItemButtonSingle in craftItemButtonSingleList)
        {
            craftItemButtonSingle.GetButton().onClick.AddListener(() => CraftItemButton_OnClick(craftItemButtonSingle));
        }
    }
    private void CraftItemButton_OnClick(CraftingItemButtonSingle selectedButton)
    {

        foreach (CraftingItemButtonSingle craftItemButton in craftItemButtonSingleList)
        {
            if (selectedButton == craftItemButton)
            {
                craftItemButton.ShowSelectedGameObject();
            }
            else
            {
                craftItemButton.HideSelectedGameObject();
            }
        }
    }
    public void ResetCraftItemButton()
    {
        foreach (CraftingItemButtonSingle craftItemButton in craftItemButtonSingleList)
        {
            craftItemButton.HideSelectedGameObject();
        }
    }




}
