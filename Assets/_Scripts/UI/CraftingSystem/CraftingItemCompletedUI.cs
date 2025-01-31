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
    [SerializeField] private GreatCheckUI greatCheckUI;

    [SerializeField] private Button autoCraftButton, cancelButton, manualCraftButton, closeButton2, startManualCraftButton;
    [SerializeField] private SliderUI sliderUI;
    [SerializeField] private InventoryItemImageContainer inventoryItemImageContainer;

    private CraftingItemSO craftingItemSO;
    private int amountCraft = 1;


    public override void Awake()
    {
        base.Awake();
        messageText.GetComponent<TextEffect>().enabled = false;
    }
    private void Start()
    {
        cancelButton.onClick.AddListener(() =>
        {
            CraftingSystem.Instance.SetCraftingState(CraftingSystem.State.Canceled, craftingItemSO);
        });
        autoCraftButton.onClick.AddListener(() =>
        {
            CraftingSystem.Instance.SetCraftingState(CraftingSystem.State.AutoCrafting, craftingItemSO);
        });

        closeButton.onClick.AddListener(() =>
        {
            CraftingSystem.Instance.SetCraftingState(CraftingSystem.State.Idle, null);

        });
        closeButton2.onClick.AddListener(() =>
        {
            CraftingSystem.Instance.SetCraftingState(CraftingSystem.State.Idle, null);
            Hide();
        });
        manualCraftButton.onClick.AddListener(() =>
        {
            CraftingSystem.Instance.SetCraftingState(CraftingSystem.State.ManualConfirm, craftingItemSO);
        });
        startManualCraftButton.onClick.AddListener(() =>
        {
            CraftingSystem.Instance.SetCraftingState(CraftingSystem.State.ManualCrafting, craftingItemSO);
        });
        greatCheckUI.OnGreatCheckFinish += GreatCheck_CheckFinish;

        timerUI.OnTimerFinish += TimerUI_OnTimerFinish;

        CraftingSystem.Instance.OnCraftingStateChanged += CraftingSystem_OnStateChanged;
        sliderUI.GetSlider().onValueChanged.AddListener(SliderUI_OnValueChange);



        Hide();
    }
    private void GreatCheck_CheckFinish(object sender, GreatCheckUI.OnGreatCheckFinishEventArgs args)
    {
        if (CraftingSystem.Instance.state == CraftingSystem.State.ManualCrafting)
        {
            float purpleRate = args.rateDict[GreatCheckUI.Status.Perfect];
            float yellowRate = args.rateDict[GreatCheckUI.Status.Good];
            float whiteRate = args.rateDict[GreatCheckUI.Status.Failure];
            CraftingSystem.Instance.SetRateColorDict(purpleRate, yellowRate, 0f, whiteRate);
            CraftingSystem.Instance.Craft(craftingItemSO, amountCraft);
            CraftingSystem.Instance.SetCraftingState(CraftingSystem.State.Completed, craftingItemSO);
        }
    }
    private void TimerUI_OnTimerFinish(object sender, EventArgs eventArgs)
    {

        if (CraftingSystem.Instance.state == CraftingSystem.State.AutoCrafting)
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
        greatCheckUI.OnGreatCheckFinish -= GreatCheck_CheckFinish;
    }

    private void CraftingSystem_OnStateChanged(object sender, CraftingSystem.OnCraftingStateChangedEventArgs args)
    {

        int minAmountCraft = 1;
        switch (args.state)
        {
            case CraftingSystem.State.Idle:

                sliderUI.ResetSlider();
                inventoryItemImageContainer.Hide();
                inventoryItemImage.Show();
                closeButton.enabled = true;

                break;

            case CraftingSystem.State.Confirm:
                amountCraft = minAmountCraft;
                craftingItemSO = args.craftingItemSO;
                sliderUI.SetSliderMaxValue(CraftingSystem.Instance.GetMaxAmountCraftItem(craftingItemSO.craftingRequiredItemSOList));
                inventoryItemImage.SetCraftItem(craftingItemSO.inventoryItemSO.sprite, minAmountCraft);
                SetConfirmMessageText();
                cancelButton.gameObject.SetActive(false);
                closeButton2.gameObject.SetActive(false);
                startManualCraftButton.gameObject.SetActive(false);
                manualCraftButton.gameObject.SetActive(true);
                autoCraftButton.gameObject.SetActive(true);



                timerUI.Hide();
                sliderUI.Show();
                Show();
                break;
            case CraftingSystem.State.ManualConfirm:
                SetManualCraftMessageText();
                sliderUI.Hide();
                inventoryItemImage.Show();
                manualCraftButton.gameObject.SetActive(false);
                autoCraftButton.gameObject.SetActive(false);
                startManualCraftButton.gameObject.SetActive(true);
                break;

            case CraftingSystem.State.AutoCrafting:
                SetAutoCraftingMessageText();
                timerUI.SetStart(CraftingSystem.craftingTimerMax);
                sliderUI.Hide();
                inventoryItemImage.Hide();
                autoCraftButton.gameObject.SetActive(false);
                manualCraftButton.gameObject.SetActive(false);
                cancelButton.gameObject.SetActive(true);
                closeButton.enabled = false;
                break;

            case CraftingSystem.State.ManualCrafting:
                SetManualCraftMessageText();
                inventoryItemImage.Hide();
                greatCheckUI.StartRound(amountCraft);
                startManualCraftButton.gameObject.SetActive(false);
                closeButton.enabled = false;
                break;

            case CraftingSystem.State.Completed:

                SetCompletedMessageText();
                timerUI.SetFinish();
                inventoryItemImageContainer.Show();
                inventoryItemImageContainer.UpdateItemImage(craftingItemSO.inventoryItemSO);



                cancelButton.gameObject.SetActive(false);
                autoCraftButton.gameObject.SetActive(false);
                manualCraftButton.gameObject.SetActive(false);
                closeButton2.gameObject.SetActive(true);

                closeButton.enabled = true;



                break;

            case CraftingSystem.State.Canceled:

                SetCanceledMessageText();
                inventoryItemImage.Show();
                timerUI.SetFinish();
                autoCraftButton.gameObject.SetActive(false);
                cancelButton.gameObject.SetActive(false);
                manualCraftButton.gameObject.SetActive(false);
                closeButton2.gameObject.SetActive(true);
                closeButton.enabled = true;
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
    private void SetManualCraftMessageText()
    {
        messageText.text = $"Manually crafting x{amountCraft} {craftingItemSO.inventoryItemSO.itemName}...";
        messageText.color = Color.black;
        messageText.GetComponent<TextEffect>().enabled = false;
    }
    private void SetCompletedMessageText()
    {
        messageText.text = $"Crafted x{amountCraft} {craftingItemSO.inventoryItemSO.itemName} completed";
        messageText.color = Color.green;
        messageText.GetComponent<TextEffect>().enabled = false;
    }
    private void SetCanceledMessageText()
    {
        messageText.text = $"Crafting x{amountCraft} {craftingItemSO.inventoryItemSO.itemName} canceled";
        messageText.color = Color.red;
        messageText.GetComponent<TextEffect>().enabled = false;
    }
    private void SetAutoCraftingMessageText()
    {
        messageText.text = $"Automatically crafting x{amountCraft} {craftingItemSO.inventoryItemSO.itemName}...";
        messageText.color = Color.black;
        messageText.GetComponent<TextEffect>().enabled = true;
    }






}
