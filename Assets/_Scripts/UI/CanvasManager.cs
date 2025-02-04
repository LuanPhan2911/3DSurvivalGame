using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private List<ICanvasManager> UIList;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TargetPointUI targetPointUI;

    private bool isUIOpen;

    public static CanvasManager Instance { get; private set; }

    public float GetScaler()
    {
        return canvas.scaleFactor;
    }

    private void Awake()
    {
        Instance = this;
        UIList = new List<ICanvasManager>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.TryGetComponent(out ICanvasManager UI))
            {
                UIList.Add(UI);
            }
        }
    }
    private void Start()
    {
        foreach (ICanvasManager UI in UIList)
        {
            UI.OnUIOpenChanged += UI_OnOpenChanged;
        }
    }
    private void OnDestroy()
    {
        foreach (ICanvasManager UI in UIList)
        {
            UI.OnUIOpenChanged -= UI_OnOpenChanged;
        }
    }
    private void UI_OnOpenChanged(object sender, EventArgs args)
    {
        CheckUIOpenToSetLookAround();
    }

    // private void Update()
    // {
    //     CheckUIOpenToSetLookAround();
    // }
    private void CheckUIOpenToSetLookAround()
    {
        UpdateIsOpenUI();
        if (isUIOpen)
        {
            targetPointUI.Hide();
            MouseMovement.Instance.DisabledLookAround();
            GameInput.Instance.DisableJump();
            GameInput.Instance.DisableAttack();

        }
        else
        {
            targetPointUI.Show();
            MouseMovement.Instance.EnableLookAround();
            GameInput.Instance.EnableJump();
            GameInput.Instance.EnableAttack();
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
