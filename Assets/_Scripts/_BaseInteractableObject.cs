using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseInteractableObject : MonoBehaviour
{
    [SerializeField] protected OriginalObjectSO originalObjectSO;

    private bool isPlayerInRange;
    protected int Hp;

    private void Awake()
    {
        Hp = originalObjectSO.maxHP;
    }

    private void Start()
    {
        GameInput.Instance.OnAttackAction += GameInput_OnAttackAction;


    }

    protected void SetOriginalObjectHP(int changedHpValue)
    {
        Hp = Mathf.Clamp(Hp - changedHpValue, 0, originalObjectSO.maxHP);
    }
    protected abstract void GameInput_OnAttackAction(object sender, EventArgs eventArgs);
    private void OnDestroy()
    {
        GameInput.Instance.OnAttackAction -= GameInput_OnAttackAction;
    }

    protected virtual int GetAmountItemProvided()
    {
        int bonus = UnityEngine.Random.Range(0, 2);
        float rate = (float)Player.Instance.GetDamage() / originalObjectSO.maxHP;
        return Mathf.RoundToInt(rate * originalObjectSO.maxAmountItemProvided) + bonus;
    }
    protected virtual bool IsNeedDestroy()
    {
        return Hp == 0;
    }
    public OriginalObjectSO GetOriginalObjectSO()
    {
        return originalObjectSO;
    }
    public InventoryItemSO GetInventoryItemSO()
    {
        return originalObjectSO.inventoryItemSO;
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

    public bool IsPlayerInRange()
    {
        return isPlayerInRange;
    }
    public bool IsCanPickedUp()
    {
        return originalObjectSO.canPickedUp;
    }
    public virtual bool IsCanInteract()
    {
        return isPlayerInRange && TargetPointUI.Instance.IsTarget(this);
    }



}
