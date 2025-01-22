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

    public event EventHandler OnButtonClick;

    private void Awake()
    {

        button.onClick.AddListener(() =>
        {
            craftingItemUI.SetCraftingItemSO(craftingItemSO);
            craftingItemUI.Show();
            OnButtonClick?.Invoke(this, EventArgs.Empty);
        });
        HideSelectedGameObject();
    }
    private void Start()
    {
        image.sprite = craftingItemSO.itemImage;
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
