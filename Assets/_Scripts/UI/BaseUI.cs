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

    public void ToggleIsOpen()
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

    public void Show()
    {
        isOpen = true;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        isOpen = false;
        gameObject.SetActive(false);
    }

    public bool GetIsOpen()
    {
        return isOpen;
    }

}
