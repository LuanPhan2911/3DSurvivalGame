using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class GameInput : MonoBehaviour
{


    public InputAction inventoryUIAction;

    public static GameInput Instance { get; private set; }

    public enum PlayerActionEnum
    {
        Move,
        Look,
        Jump,
        Attack,
        Interact,
        Sprint
    }
    public enum UIActionEnum
    {
        Inventory,
        QuickSlot
    }

    public Dictionary<PlayerActionEnum, InputAction> playerInputActionDict;
    public Dictionary<UIActionEnum, InputAction> UIInputActionDict;

    public EventHandler OnOpenInventoryAction;

    public EventHandler OnAttackAction;
    public EventHandler OnInteractAction;

    public class OnQuickSlotActionEventArgs : EventArgs
    {
        public int slotIndex;
    }
    public event EventHandler<OnQuickSlotActionEventArgs> OnQuickSlotAction;



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
        playerInputActionDict[PlayerActionEnum.Attack].performed += AttackAction;

        UIInputActionDict[UIActionEnum.Inventory].performed += InventoryUIAction;
        UIInputActionDict[UIActionEnum.QuickSlot].performed += QuickSlotUIAction;



    }
    private void OnDestroy()
    {
        playerInputActionDict[PlayerActionEnum.Attack].performed -= AttackAction;

        UIInputActionDict[UIActionEnum.Inventory].performed -= InventoryUIAction;
        UIInputActionDict[UIActionEnum.QuickSlot].performed -= QuickSlotUIAction;
    }

    private void InventoryUIAction(InputAction.CallbackContext callback)
    {
        OnOpenInventoryAction?.Invoke(this, EventArgs.Empty);
    }
    private void QuickSlotUIAction(InputAction.CallbackContext callback)
    {
        string keyname = callback.control.name;
        if (int.TryParse(keyname, out int slotIndex))
        {
            OnQuickSlotAction?.Invoke(this, new OnQuickSlotActionEventArgs
            {
                slotIndex = slotIndex
            });
        }
    }
    private void AttackAction(InputAction.CallbackContext callback)
    {
        OnAttackAction?.Invoke(this, EventArgs.Empty);
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
    public bool IsSprintActionPressed()
    {
        return playerInputActionDict[PlayerActionEnum.Sprint].IsPressed();
    }
    public bool IsInventoryActionPress()
    {
        return UIInputActionDict[UIActionEnum.Inventory].IsPressed();
    }
    public void DisableJump()
    {
        playerInputActionDict[PlayerActionEnum.Jump].Disable();
    }
    public void EnableJump()
    {
        playerInputActionDict[PlayerActionEnum.Jump].Enable();
    }
    public void DisableAttack()
    {
        playerInputActionDict[PlayerActionEnum.Attack].Disable();
    }
    public void EnableAttack()
    {
        playerInputActionDict[PlayerActionEnum.Attack].Enable();
    }






}
