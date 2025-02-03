using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmUI : MonoBehaviour
{
    public static ConfirmUI Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI confirmText;
    [SerializeField] private Button cancelButton, confirmButton;

    private Action ConfirmAction;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        cancelButton.onClick.AddListener(() =>
        {
            Hide();
            ConfirmAction = null;
        });
        confirmButton.onClick.AddListener(() =>
        {
            ConfirmAction?.Invoke();
            Hide();
            ConfirmAction = null;
        });
        Hide();
    }


    public void ShowConfirm(string message, Action ConfirmAction)
    {
        Show();
        confirmText.text = message;
        this.ConfirmAction = ConfirmAction;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
