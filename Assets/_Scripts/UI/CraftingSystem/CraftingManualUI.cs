using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManualUI : BaseUI
{

    [SerializeField] private Button startButton;
    [SerializeField] private GreatCheckUI greatCheckUI;


    private void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            greatCheckUI.StartRound(10);
            startButton.enabled = false; ;
            closeButton.enabled = false;
        });

        greatCheckUI.OnGreatCheckFinish += GreatCheck_OnFinish;
        Hide();
        ToggleIsOpen();
    }
    private void GreatCheck_OnFinish(object sender, GreatCheckUI.OnGreatCheckFinishEventArgs args)
    {
        startButton.enabled = true;
        closeButton.enabled = true;
    }
}
