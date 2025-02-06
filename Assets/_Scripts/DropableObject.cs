using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropableObject : BaseInteractableObject
{
    [SerializeField] private ParticleSystem dropParticle;
    [SerializeField] private float delayDestroy = 1f;
    protected override void GameInput_OnAttackAction(object sender, EventArgs eventArgs)
    {
        if (IsCanInteract())
        {
            dropParticle.Play();
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

    protected override int GetAmountItemProvided()
    {
        int bonus = UnityEngine.Random.Range(0, 3);
        float rate = (float)Player.Instance.GetDamage() / originalObjectSO.maxHP;
        return Mathf.RoundToInt(rate * originalObjectSO.maxAmountItemProvided) + bonus;
    }

    protected override bool IsNeedDestroy()
    {
        return Hp == 0;
    }


}
