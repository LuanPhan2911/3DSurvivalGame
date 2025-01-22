using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private InventoryItemSO inventoryItemSO;

    private bool isPlayerInRange;

    private void Start()
    {
        GameInput.Instance.OnAttackAction += GameInput_OnAttackAction;
    }
    private void GameInput_OnAttackAction(object sender, EventArgs eventArgs)
    {
        if (IsCanInteract())
        {
            InventorySystem.Instance.AddToInventory(this);
        }
    }
    private void OnDestroy()
    {
        GameInput.Instance.OnAttackAction -= GameInput_OnAttackAction;
    }

    private bool IsCanInteract()
    {
        return isPlayerInRange && TargetPointUI.Instance.GetIsTarget() &&
         TargetPointUI.Instance.GetInteractionGameObject() == gameObject;
    }
    public InventoryItemSO GetInventoryItemSO()
    {
        return inventoryItemSO;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    public bool GetIsPlayerInRange()
    {
        return isPlayerInRange;
    }



}
