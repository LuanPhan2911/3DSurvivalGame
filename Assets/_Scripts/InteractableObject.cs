using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private InteractableObjectSO interactableObjectSO;

    private bool isPlayerInRange;

    private void Start()
    {
        GameInput.Instance.OnAttackAction += (sender, args) =>
        {
            if (IsCanPickup())
            {
                InventorySystem.Instance.AddToInventory(this);
            }
        };
    }

    private bool IsCanPickup()
    {
        return isPlayerInRange && SelectionManager.Instance.GetIsTarget();
    }
    public string GetItemName()
    {
        return interactableObjectSO.itemName;
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
    public InteractableObjectSO GetInteractableObjectSO()
    {
        return interactableObjectSO;
    }


}
