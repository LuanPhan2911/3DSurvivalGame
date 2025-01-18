using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI interactInfoText;



    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Transform selectionTransform = hit.transform;

            if (selectionTransform.TryGetComponent(out InteractableObject interactObject))
            {
                interactInfoText.text = interactObject.GetItemName();
                interactInfoText.gameObject.SetActive(true);
            }
            else
            {
                interactInfoText.gameObject.SetActive(false);
            }

        }
    }

}
