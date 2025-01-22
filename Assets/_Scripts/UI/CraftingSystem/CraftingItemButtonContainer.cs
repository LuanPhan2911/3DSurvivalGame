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
            craftItemButtonSingle.OnButtonClick += CraftItemButton_OnClick;
        }
    }
    private void CraftItemButton_OnClick(object sender, EventArgs eventArgs)
    {
        CraftingItemButtonSingle selectedButton = sender as CraftingItemButtonSingle;
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
    private void OnDestroy()
    {
        foreach (CraftingItemButtonSingle craftItemButtonSingle in craftItemButtonSingleList)
        {
            craftItemButtonSingle.OnButtonClick -= CraftItemButton_OnClick;
        }
    }



}
