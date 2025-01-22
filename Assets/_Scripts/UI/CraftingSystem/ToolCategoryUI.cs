using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolCategoryUI : BaseUI
{
    [SerializeField] private Button backButton;
    [SerializeField] private BaseUI backUI;


    public override void Awake()
    {
        base.Awake();
        backButton.onClick.AddListener(() =>
        {
            Hide();
            backUI.Show();
        });
    }
    private void Start()
    {
        Hide();
    }
}
