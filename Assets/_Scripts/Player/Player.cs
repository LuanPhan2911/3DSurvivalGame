using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    [SerializeField] private Transform toolHolderTransform;
    [SerializeField] private PlayerVisual playerVisual;

    private EquippableTool equippableTool;





    private int damage = 5;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InventorySystem.Instance.OnEquippableItemSelected += InventorySystem_OnEquippableItemSelected;
        GameInput.Instance.OnAttackAction += GameInput_OnAttack;
    }
    private void OnDestroy()
    {
        InventorySystem.Instance.OnEquippableItemSelected -= InventorySystem_OnEquippableItemSelected;
        GameInput.Instance.OnAttackAction -= GameInput_OnAttack;
    }
    private void GameInput_OnAttack(object sender, EventArgs eventArgs)
    {
        if (HasEquippableTool())
        {
            BaseInteractableObject baseInteractableObject = TargetPointUI.Instance.GetInteractableObject();
            if (baseInteractableObject && baseInteractableObject.IsCanPickedUp())
            {
                // can pick up item
                // playerVisual.StartAnimation();
            }
            else
            {
                equippableTool.StartAnimation();
                //equip tool
            }

        }
        else
        {
            // not equip tool
            // playerVisual.StartAnimation();
        }

    }
    private void InventorySystem_OnEquippableItemSelected(object sender, InventorySystem.OnEquippableItemSelectedEventArgs args)
    {
        if (args.inventoryItem)
        {
            DestroyEquippableTool();
            InstantiateEquippableTool(args.inventoryItem.itemModel.transform);
        }
        else
        {
            DestroyEquippableTool();
        }
    }

    public int GetDamage()
    {
        int bonus = UnityEngine.Random.Range(0, 2);
        return damage + bonus;
    }
    private void InstantiateEquippableTool(Transform equippableTransformPrefab)
    {
        Transform equippableTransform = Instantiate(equippableTransformPrefab, toolHolderTransform);
        equippableTool = equippableTransform.GetComponent<EquippableTool>();
    }
    private void DestroyEquippableTool()
    {
        if (equippableTool)
        {
            Destroy(equippableTool.gameObject);
            equippableTool = null;
        }
    }
    public bool HasEquippableTool()
    {
        return equippableTool != null;
    }
    public PlayerVisual GetPlayerVisual()
    {
        return playerVisual;
    }

}
