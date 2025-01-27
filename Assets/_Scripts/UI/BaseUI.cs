using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour, ICanvasManager
{
    protected bool isOpen = false;
    [SerializeField] protected Button closeButton;



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
    public virtual void ToggleIsOpen(Action<bool> callBack)
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
        callBack(isOpen);
    }

    public virtual void Show()
    {
        isOpen = true;
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        isOpen = false;
        gameObject.SetActive(false);
    }

    public bool GetIsOpen()
    {
        return isOpen;
    }

}
