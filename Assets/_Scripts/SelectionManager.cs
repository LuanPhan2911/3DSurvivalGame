using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{

    public static SelectionManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI interactInfoText;

    private bool isTarget;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
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

                    interactInfoText.text = interactObject.GetItemName();
                    interactInfoText.gameObject.SetActive(true);
                    isTarget = true;
                }

            }
            else
            {
                interactInfoText.gameObject.SetActive(false);
                isTarget = false;
            }

        }
        else
        {
            // don't hit anything, hidden selection information
            interactInfoText.gameObject.SetActive(false);
            isTarget = false;
        }
    }

    public bool GetIsTarget()
    {
        return isTarget;
    }

}
