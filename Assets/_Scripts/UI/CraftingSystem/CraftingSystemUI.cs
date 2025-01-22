using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystemUI : BaseUI
{

    private void Start()
    {
        GameInput.Instance.OnOpenCraftingAction += (sender, args) =>
        {
            ToggleIsOpen();

        };
        Hide();
    }

}
