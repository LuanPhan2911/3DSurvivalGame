using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCraftTabUI : BaseUI
{
    [SerializeField] private NavigationTabButton[] navigationTabButtonArray;
    [SerializeField] private NavigationTabButton activeTabButton;
    [SerializeField] private CraftingItemUI craftingItemUI;


    private void Start()
    {
        foreach (NavigationTabButton navigationTabButton in navigationTabButtonArray)
        {
            navigationTabButton.GetButton().onClick.AddListener(() =>
            {
                TabButtonOnClick(navigationTabButton);
            });
        }
        GameInput.Instance.OnOpenInventoryAction += GameInput_OpenInventory;
        TabButtonOnClick(activeTabButton);
        Hide();
    }
    private void OnDestroy()
    {
        GameInput.Instance.OnOpenInventoryAction -= GameInput_OpenInventory;
    }
    private void GameInput_OpenInventory(object sender, EventArgs eventArgs)
    {
        ToggleIsOpen();
    }
    public override void Hide()
    {
        if (craftingItemUI.GetIsOpen())
        {
            craftingItemUI.Hide();
        }
        base.Hide();
    }



    private void TabButtonOnClick(NavigationTabButton clickedTabButton)
    {

        foreach (NavigationTabButton navigationTabButton in navigationTabButtonArray)
        {
            if (navigationTabButton == clickedTabButton)
            {
                navigationTabButton.ShowSelectedGameObject();
                navigationTabButton.ShowTabContent();
            }
            else
            {
                navigationTabButton.HideSelectedGameObject();
                navigationTabButton.HideTabContent();
            }
        }
    }



}
