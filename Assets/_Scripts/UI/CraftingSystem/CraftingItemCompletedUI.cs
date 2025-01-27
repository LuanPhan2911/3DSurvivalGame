using System;
using System.Collections;
using System.Collections.Generic;
using EasyTextEffects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingItemCompletedUI : BaseUI
{
    [SerializeField] private TimerUI timerUI;
    [SerializeField] private TextMeshProUGUI messageText;

    [SerializeField] private InventoryItemImage inventoryItemImage;



    [SerializeField] private Button confirmButton, cancelButton, hideButton;
    [SerializeField] private SliderUI sliderUI;

    private CraftingItemSO craftingItemSO;
    private int amountCraft = 1;
    private Vector3 hideButtonOldPosition;

    public override void Awake()
    {
        base.Awake();
        hideButtonOldPosition = hideButton.transform.position;
        messageText.GetComponent<TextEffect>().enabled = false;
    }
    private void Start()
    {
        cancelButton.onClick.AddListener(() =>
        {
            CraftingSystem.Instance.SetCraftingState(CraftingSystem.State.Canceled, craftingItemSO);
        });
        confirmButton.onClick.AddListener(() =>
        {
            CraftingSystem.Instance.SetCraftingState(CraftingSystem.State.Crafting, craftingItemSO);
        });

        closeButton.onClick.AddListener(() =>
        {
            CraftingSystem.Instance.SetCraftingState(CraftingSystem.State.Idle, null);

        });
        hideButton.onClick.AddListener(() =>
        {
            CraftingSystem.Instance.SetCraftingState(CraftingSystem.State.Idle, null);
            Hide();
        });

        timerUI.OnTimerFinish += TimerUI_OnTimerFinish;

        CraftingSystem.Instance.OnCraftingStateChanged += CraftingSystem_OnStateChanged;
        sliderUI.GetSlider().onValueChanged.AddListener(SliderUI_OnValueChange);



        Hide();
    }
    private void TimerUI_OnTimerFinish(object sender, EventArgs eventArgs)
    {
        if (CraftingSystem.Instance.state == CraftingSystem.State.Crafting)
        {
            CraftingSystem.Instance.Craft(craftingItemSO, amountCraft);
            CraftingSystem.Instance.SetCraftingState(CraftingSystem.State.Completed, craftingItemSO);
        }
    }
    private void SliderUI_OnValueChange(float amount)
    {
        amountCraft = (int)amount;
        inventoryItemImage.SetCraftItem(craftingItemSO.inventoryItemSO.sprite, amountCraft);
        SetConfirmMessageText();
    }
    private void OnDestroy()
    {
        CraftingSystem.Instance.OnCraftingStateChanged -= CraftingSystem_OnStateChanged;
        timerUI.OnTimerFinish -= TimerUI_OnTimerFinish;
    }

    private void CraftingSystem_OnStateChanged(object sender, CraftingSystem.OnCraftingStateChangedEventArgs args)
    {

        int minAmountCraft = 1;
        switch (args.state)
        {
            case CraftingSystem.State.Idle:
                amountCraft = minAmountCraft;
                sliderUI.ResetSlider();
                hideButton.transform.position = hideButtonOldPosition;
                break;

            case CraftingSystem.State.Confirm:
                craftingItemSO = args.craftingItemSO;
                sliderUI.SetSliderMaxValue(CraftingSystem.Instance.GetMaxAmountCraftItem(craftingItemSO.craftingRequiredItemSOList));
                inventoryItemImage.SetCraftItem(craftingItemSO.inventoryItemSO.sprite, minAmountCraft);
                SetConfirmMessageText();
                cancelButton.gameObject.SetActive(false);
                hideButton.gameObject.SetActive(true);
                confirmButton.gameObject.SetActive(true);

                timerUI.Hide();
                sliderUI.Show();
                Show();
                break;

            case CraftingSystem.State.Crafting:
                SetCraftingMessageText();
                timerUI.SetStart(CraftingSystem.craftingTimerMax);
                sliderUI.Hide();
                inventoryItemImage.Hide();
                confirmButton.gameObject.SetActive(false);
                hideButton.gameObject.SetActive(false);
                cancelButton.gameObject.SetActive(true);
                break;

            case CraftingSystem.State.Completed:

                SetCompletedMessageText();
                inventoryItemImage.Show();
                timerUI.SetFinish();
                confirmButton.gameObject.SetActive(false);
                cancelButton.gameObject.SetActive(false);
                hideButton.gameObject.SetActive(true);

                //update hide button position to cancel button position
                hideButton.transform.position = cancelButton.transform.position;

                break;

            case CraftingSystem.State.Canceled:

                SetFailedMessageText();
                inventoryItemImage.Show();
                timerUI.SetFinish();
                confirmButton.gameObject.SetActive(false);
                cancelButton.gameObject.SetActive(false);
                hideButton.gameObject.SetActive(true);
                //update hide button position to cancel button position
                hideButton.transform.position = cancelButton.transform.position;

                break;

            default:
                break;
        }
    }




    private void SetConfirmMessageText()
    {
        messageText.text = $"Craft x{amountCraft} {craftingItemSO.inventoryItemSO.itemName}";
        messageText.color = Color.black;
        messageText.GetComponent<TextEffect>().enabled = false;
    }
    private void SetCompletedMessageText()
    {
        messageText.text = $"Crafted x{amountCraft} {craftingItemSO.inventoryItemSO.itemName} completed";
        messageText.color = Color.green;
        messageText.GetComponent<TextEffect>().enabled = false;
    }
    private void SetFailedMessageText()
    {
        messageText.text = $"Craft x{amountCraft} {craftingItemSO.inventoryItemSO.itemName} failed";
        messageText.color = Color.red;
        messageText.GetComponent<TextEffect>().enabled = false;
    }
    private void SetCraftingMessageText()
    {
        messageText.text = $"Crafting x{amountCraft} {craftingItemSO.inventoryItemSO.itemName}...";
        messageText.color = Color.black;
        messageText.GetComponent<TextEffect>().enabled = true;
    }






}
