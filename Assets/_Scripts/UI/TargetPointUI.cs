using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class TargetPointUI : MonoBehaviour
{
    public static TargetPointUI Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI infoText;



    private bool isTarget;

    private GameObject interactedGameObject;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        HandleTargetObject();
    }
    private void HandleTargetObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Transform selectionTransform = hit.transform;

            if (selectionTransform.TryGetComponent(out InteractableObject interactObject))
            {

                if (interactObject.GetIsPlayerInRange())
                {
                    //player in range that can interact to object

                    interactedGameObject = interactObject.gameObject;
                    isTarget = true;

                    infoText.text = interactObject.GetInventoryItemSO().originatedFromObjectName;
                    infoText.gameObject.SetActive(true);

                }

            }
            else
            {
                infoText.gameObject.SetActive(false);
                isTarget = false;
            }

        }
        else
        {
            // don't hit anything, hidden selection information
            infoText.gameObject.SetActive(false);
            isTarget = false;
        }
    }
    public GameObject GetInteractionGameObject()
    {
        return interactedGameObject;
    }

    public bool GetIsTarget()
    {
        return isTarget;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
