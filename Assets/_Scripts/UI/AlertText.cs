using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlertText : MonoBehaviour
{


    [SerializeField] private float timerDisplayMax = 2f;
    [SerializeField] private int speed = 100;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private float timerDisplay;

    private void Update()
    {
        if (timerDisplay < timerDisplayMax)
        {
            timerDisplay += Time.deltaTime;
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else
        {
            timerDisplay = 0f;
            Destroy(gameObject);

        }
    }

    public void SetAlertText(string text)
    {
        textMeshPro.text = text;
    }




}
