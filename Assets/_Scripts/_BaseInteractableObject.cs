using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    protected abstract int GetAmountItemProvided();
    protected abstract bool IsNeedDestroy();



    public virtual bool IsCanInteract()
    {
        return isPlayerInRange && TargetPointUI.Instance.GetIsTarget() &&
         TargetPointUI.Instance.GetInteractionGameObject() == gameObject &&
         TargetPointUI.Instance.gameObject.activeSelf;
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



}
