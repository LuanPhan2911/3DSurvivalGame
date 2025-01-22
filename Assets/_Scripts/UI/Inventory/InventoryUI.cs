using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : BaseUI
{


    private void Start()
    {
        GameInput.Instance.OnOpenInventoryAction += (sender, args) =>
        {
            ToggleIsOpen();

        };
        Hide();
    }

}
