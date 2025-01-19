using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    private bool isOpen = false;

    private void Start()
    {
        GameInput.Instance.OnToggleInventoryAction += (sender, args) =>
        {
            ToggleIsOpen();
            if (isOpen)
            {
                Show();
            }
            else
            {
                Hide();
            }
        };
        Hide();
    }

    private void ToggleIsOpen()
    {
        isOpen = !isOpen;
        // unlock cursor
        Cursor.lockState = CursorLockMode.None;

    }

    private void Show()
    {
        gameObject.SetActive(true);
        MouseMovement.Instance.DisabledLookAround();

    }

    private void Hide()
    {
        gameObject.SetActive(false);
        MouseMovement.Instance.EnableLookAround();

    }
}
