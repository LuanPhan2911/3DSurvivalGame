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
    private BaseInteractableObject interactableObject;

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
            interactableObject = selectionTransform.GetComponentInParent<BaseInteractableObject>();
            if (interactableObject && interactableObject.IsPlayerInRange())
            {
                UpdateInteractObject();
            }
            else
            {
                infoText.gameObject.SetActive(false);
                interactableObject = null;
                SetCenterImage(defaultSprite, defaultScaler);
            }

        }
        else
        {
            // don't hit anything, hidden selection information
            infoText.gameObject.SetActive(false);
            interactableObject = null;
            SetCenterImage(defaultSprite, defaultScaler);
        }
    }


    private void UpdateInteractObject()
    {

        infoText.text = interactableObject.GetOriginalObjectSO().objectName;
        infoText.gameObject.SetActive(true);

        // update cursor hand if can pick up
        if (interactableObject.IsCanPickedUp())
        {
            SetCenterImage(handSprite, Vector3.one);
        }
        else
        {
            SetCenterImage(defaultSprite, defaultScaler);
        }
    }

    public BaseInteractableObject GetInteractableObject()
    {
        return interactableObject;
    }
    public bool IsTarget(BaseInteractableObject baseInteractableObject)
    {
        return interactableObject == baseInteractableObject;
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
