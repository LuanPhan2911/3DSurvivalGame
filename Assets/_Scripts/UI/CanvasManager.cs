using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private List<ICanvasManager> UIList;

    private bool isUIOpen;

    private void Awake()
    {
        UIList = new List<ICanvasManager>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.TryGetComponent(out ICanvasManager UI))
            {
                UIList.Add(UI);
            }
        }
    }

    private void Update()
    {
        CheckUIOpenToSetLookAround();
    }
    private void CheckUIOpenToSetLookAround()
    {
        UpdateIsOpenUI();
        if (!isUIOpen)
        {
            MouseMovement.Instance.EnableLookAround();
        }
        else
        {
            MouseMovement.Instance.DisabledLookAround();
        }
    }
    private void UpdateIsOpenUI()
    {


        foreach (ICanvasManager UI in UIList)
        {
            if (UI.GetIsOpen())
            {
                isUIOpen = true;
                return;
            }
        }
        isUIOpen = false;

    }


}
