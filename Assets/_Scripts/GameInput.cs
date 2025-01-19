using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{


    public InputAction inventoryUIAction;

    public static GameInput Instance { get; private set; }

    public enum PlayerActionEnum
    {
        Move,
        Look,
        Jump,
    }
    public enum UIActionEnum
    {
        Inventory
    }

    public Dictionary<PlayerActionEnum, InputAction> playerInputActionDict;
    public Dictionary<UIActionEnum, InputAction> UIInputActionDict;

    public EventHandler OnToggleInventoryAction;



    private void Awake()
    {
        Instance = this;

        playerInputActionDict = new Dictionary<PlayerActionEnum, InputAction>();
        UIInputActionDict = new Dictionary<UIActionEnum, InputAction>();
        foreach (PlayerActionEnum playerAction in Enum.GetValues(typeof(PlayerActionEnum)))
        {
            playerInputActionDict.Add(playerAction, InputSystem.actions.FindAction(playerAction.ToString()));
        }
        foreach (UIActionEnum UIAction in Enum.GetValues(typeof(UIActionEnum)))
        {
            UIInputActionDict.Add(UIAction, InputSystem.actions.FindAction(UIAction.ToString()));
        }

    }
    private void Start()
    {
        UIInputActionDict[UIActionEnum.Inventory].performed += InventoryUIAction;
    }
    private void OnDestroy()
    {
        UIInputActionDict[UIActionEnum.Inventory].performed -= InventoryUIAction;
    }

    private void InventoryUIAction(InputAction.CallbackContext callback)
    {
        OnToggleInventoryAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActionDict[PlayerActionEnum.Move].ReadValue<Vector2>();

        inputVector.Normalize();
        return inputVector;
    }

    public Vector2 GetLookAroundVector()
    {
        Vector2 inputVector = playerInputActionDict[PlayerActionEnum.Look].ReadValue<Vector2>();
        return inputVector;
    }

    public bool IsJumpActionPressed()
    {
        return playerInputActionDict[PlayerActionEnum.Jump].IsPressed();
    }
    public bool IsInventoryActionPress()
    {
        return UIInputActionDict[UIActionEnum.Inventory].IsPressed();
    }



}
