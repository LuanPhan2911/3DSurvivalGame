using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlertCheck : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textMeshPro;
    private float displayTimerMax = 2f;
    private float displayTimer = 0f;

    private void Start()
    {
        Hide();
    }

    private void Update()
    {
        if (displayTimer < displayTimerMax)
        {
            displayTimer += Time.deltaTime;
        }
        else
        {
            Hide();
        }
    }
    public void AlertSkillCheck(string text)
    {
        displayTimer = 0f;
        textMeshPro.text = text;
        Show();
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
