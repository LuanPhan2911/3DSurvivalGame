using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetPointUI : MonoBehaviour
{
    public static TargetPointUI Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite handSprite;
    [SerializeField] private Image centerImage;
    private Vector3 defaultScaler;






    private bool isTarget;

    private GameObject interactedGameObject;

    private void Awake()
    {
        Instance = this;
        centerImage.sprite = defaultSprite;
        defaultScaler = centerImage.transform.localScale;

    }

    private void SetCenterImage(Sprite sprite, Vector3 scaler)
    {
        centerImage.sprite = sprite;
        centerImage.transform.localScale = scaler;
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
            BaseInteractableObject interactableObject = selectionTransform.GetComponentInParent<BaseInteractableObject>();

            if (interactableObject && interactableObject.IsPlayerInRange())
            {
                UpdateInteractObject(interactableObject);
            }
            else
            {
                infoText.gameObject.SetActive(false);
                isTarget = false;

                SetCenterImage(defaultSprite, defaultScaler);
            }

        }
        else
        {
            // don't hit anything, hidden selection information
            infoText.gameObject.SetActive(false);
            isTarget = false;
            SetCenterImage(defaultSprite, defaultScaler);
        }
    }


    private void UpdateInteractObject(BaseInteractableObject interactObject)
    {


        interactedGameObject = interactObject.gameObject;
        isTarget = true;

        infoText.text = interactObject.GetOriginalObjectSO().objectName;
        infoText.gameObject.SetActive(true);

        // update cursor hand if can pick up
        if (interactObject.IsCanPickedUp())
        {
            SetCenterImage(handSprite, Vector3.one);
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
