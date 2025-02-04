using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{


    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private CanvasGroup canvasGroup;


    public static GameObject itemBeingDragged;
    private Vector3 startPosition;
    private Transform startParent;

    public event EventHandler OnDragStart;






    public void OnBeginDrag(PointerEventData eventData)
    {


        canvasGroup.alpha = .6f;
        //So the ray cast will ignore the item itself.
        canvasGroup.blocksRaycasts = false;
        startPosition = transform.position;
        startParent = transform.parent;
        transform.SetParent(transform.root);
        itemBeingDragged = gameObject;

        OnDragStart?.Invoke(this, EventArgs.Empty);

    }

    public void OnDrag(PointerEventData eventData)
    {
        //So the item will move with our mouse (at same speed)  and so it will be consistant if the canvas has a different scale (other then 1);
        rectTransform.anchoredPosition += eventData.delta / CanvasManager.Instance.GetScaler();

    }



    public void OnEndDrag(PointerEventData eventData)
    {

        itemBeingDragged = null;

        if (transform.parent == startParent || transform.parent == transform.root)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);

        }
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
    public Transform GetStartParentTransform()
    {
        return startParent;
    }



}