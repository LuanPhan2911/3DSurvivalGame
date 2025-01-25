using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftingItemButtonSingle : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject selectedGameObject;
    [SerializeField] private CraftingItemUI craftingItemUI;
    [SerializeField] private CraftingItemSO craftingItemSO;

    [SerializeField] private Image image;



    public Button GetButton()
    {
        return button;
    }

    private void Awake()
    {

        button.onClick.AddListener(() =>
        {
            craftingItemUI.SetCraftingItemSO(craftingItemSO);
            craftingItemUI.Show();
        });
        HideSelectedGameObject();
    }
    private void Start()
    {
        image.sprite = craftingItemSO.inventoryItemSO.sprite;
    }



    public void ShowSelectedGameObject()
    {
        selectedGameObject.SetActive(true);
    }
    public void HideSelectedGameObject()
    {
        selectedGameObject.SetActive(false);
    }



}
