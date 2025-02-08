using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeDropableObject : BaseInteractableObject
{
    [SerializeField] private ParticleSystem dropParticle;

    [SerializeField] private Animator animator;
    private const string IS_SHAKE = "IsShake";
    private float delayDestroy = 1f;
    protected override void GameInput_OnAttackAction(object sender, EventArgs eventArgs)
    {
        if (IsCanInteract())
        {
            dropParticle.Play();
            animator.SetTrigger(IS_SHAKE);
            SetOriginalObjectHP(Player.Instance.GetDamage());
            int amount = GetAmountItemProvided();
            InventorySystem.Instance.AddToInventory(this, amount);
            AlertUI.Instance.Alert(
                $"Gain x{amount} {originalObjectSO.inventoryItemSO.itemName}"
            );

            if (IsNeedDestroy())
            {
                Destroy(gameObject, delayDestroy);
            }
        }

    }




}
