using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;
    private float timerMax;
    private float timer;
    private bool isStart;
    private bool isFinish;

    public event EventHandler OnTimerFinish;

    private void Update()
    {
        if (isStart)
        {
            timer += Time.deltaTime;
            timerImage.fillAmount = (float)timer / timerMax;
            if (timer >= timerMax)
            {
                isFinish = true;
                isStart = false;
                OnTimerFinish?.Invoke(this, EventArgs.Empty);
            }
        }
        if (isFinish)
        {
            isStart = false;
            timer = 0f;
            Hide();
        }
    }
    public void SetStart(float timerMax)
    {
        this.timerMax = timerMax;
        isStart = true;
        isFinish = false;
        timer = 0f;
        Show();
    }
    public bool IsCountDown()
    {
        return isStart && !isFinish;
    }
    public void SetFinish()
    {
        isFinish = true;
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
