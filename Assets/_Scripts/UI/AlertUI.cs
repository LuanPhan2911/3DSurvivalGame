using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlertUI : MonoBehaviour
{

    public static AlertUI Instance { get; private set; }
    [SerializeField] private Transform alertTextTransformPrefab;
    [SerializeField] private float messageDelayTimer = 0.5f;
    private float messageDelay;
    private Queue<string> alertMessageQueue;


    private void Awake()
    {
        Instance = this;
        alertMessageQueue = new Queue<string>();
    }




    private void Update()
    {

        if (alertMessageQueue.Count > 0)
        {
            if (messageDelay < messageDelayTimer)
            {
                messageDelay += Time.deltaTime;
            }
            else
            {
                string alertMessage = alertMessageQueue.Peek();
                Transform alertTextTransform = Instantiate(alertTextTransformPrefab, transform);
                alertTextTransform.GetComponent<AlertText>().SetAlertText(alertMessage);

                alertMessageQueue.Dequeue();
                messageDelay = 0f;
            }
        }
        else
        {
            messageDelay = Mathf.Clamp(messageDelay + Time.deltaTime, 0f, messageDelayTimer);
        }

    }

    public void Alert(string alertMessage)
    {
        alertMessageQueue.Enqueue(alertMessage);

    }




}
