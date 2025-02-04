using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour, ICanvasManager
{
    protected bool isOpen = false;
    [SerializeField] protected Button closeButton;

    public event EventHandler OnUIOpenChanged;

    public virtual void Awake()
    {
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    public virtual void ToggleIsOpen()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }


    public virtual void Show()
    {
        gameObject.SetActive(true);
        isOpen = true;
        OnUIOpenChanged?.Invoke(this, EventArgs.Empty);

    }

    public virtual void Hide()
    {
        isOpen = false;
        OnUIOpenChanged?.Invoke(this, EventArgs.Empty);
        gameObject.SetActive(false);
    }

    public bool GetIsOpen()
    {
        return isOpen;
    }

}
