using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private OriginalObjectSO originalObjectSO;

    private bool isPlayerInRange;
    private int HP;

    private void Awake()
    {
        HP = originalObjectSO.maxHP;
    }

    private void Start()
    {
        GameInput.Instance.OnAttackAction += GameInput_OnAttackAction;
    }
    private void GameInput_OnAttackAction(object sender, EventArgs eventArgs)
    {
        if (IsCanInteract())
        {
            SetOriginalObjectHP(Player.Instance.GetDamage());
            int amount = GetAmountItemProvided();
            InventorySystem.Instance.AddToInventory(this, amount);
        }
    }
    private void OnDestroy()
    {
        GameInput.Instance.OnAttackAction -= GameInput_OnAttackAction;
    }

    private int GetAmountItemProvided()
    {
        if (originalObjectSO.maxHP == 0)
        {
            return originalObjectSO.maxAmountItemProvided;
        }
        float rate = (float)Player.Instance.GetDamage() / originalObjectSO.maxHP;
        return Mathf.RoundToInt(rate * originalObjectSO.maxAmountItemProvided);
    }


    private bool IsCanInteract()
    {
        return isPlayerInRange && TargetPointUI.Instance.GetIsTarget() &&
         TargetPointUI.Instance.GetInteractionGameObject() == gameObject;
    }
    private void SetOriginalObjectHP(int damage)
    {
        if (HP > 0)
        {
            HP -= damage;
        }

    }
    public OriginalObjectSO GetOriginalObjectSO()
    {
        return originalObjectSO;
    }
    public InventoryItemSO GetInventoryItemSO()
    {
        return originalObjectSO.inventoryItemSO;
    }
    public bool IsWhenDestroy()
    {
        return HP <= 0;
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
