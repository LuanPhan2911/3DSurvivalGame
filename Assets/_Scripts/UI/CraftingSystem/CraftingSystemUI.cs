using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystemUI : MonoBehaviour
{
    [SerializeField] private CraftingItemButtonContainer craftingItemButtonContainer;


    private void OnEnable()
    {
        craftingItemButtonContainer.ResetCraftItemButton();
    }
}
