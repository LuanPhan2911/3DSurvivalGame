using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string itemName;

    private bool isPlayerInRange;

    private void Update()
    {
        if (IsCanPickup())
        {
            Debug.Log("Pickup item");
            Destroy(gameObject);
        }

    }

    private bool IsCanPickup()
    {
        return Input.GetKeyDown(KeyCode.Mouse0)
        && isPlayerInRange && SelectionManager.Instance.GetIsTarget();
    }
    public string GetItemName()
    {
        return itemName;
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
