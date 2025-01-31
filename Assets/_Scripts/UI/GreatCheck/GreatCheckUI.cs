using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GreatCheckUI : MonoBehaviour
{
    [SerializeField] private RedCheck redCheck;
    [SerializeField] private SuccessRange successRange;
    [SerializeField] private GameObject circleImageGameObject;
    [SerializeField] private GameObject spaceImage;
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private AlertCheck alertCheck;

    public enum Status
    {
        Perfect,
        Good,
        Failure
    }
    private Dictionary<Status, int> scoreDict;

    private int roundMax = 10;
    private int roundCounter = 0;
    private int roundCounterMax;

    private float delayTimerMax = 3f;
    private float delayTimer = 0f;
    private bool isDelay = true;
    private bool isStart = false;

    public class OnGreatCheckFinishEventArgs : EventArgs
    {
        public Dictionary<Status, float> rateDict;
        public int maxRound;
    }
    public event EventHandler<OnGreatCheckFinishEventArgs> OnGreatCheckFinish;

    private void Awake()
    {
        scoreDict = new Dictionary<Status, int>();

    }
    private void Start()
    {
        redCheck.OnRedCheckFinishRotate += RedCheck_OnFinishRotate;
        redCheck.OnRedCheckFail += RedCheck_OnFail;

        scoreDict[Status.Perfect] = 0;
        scoreDict[Status.Good] = 0;
        scoreDict[Status.Failure] = 0;
        roundText.gameObject.SetActive(false);
        Hide();
    }
    private void OnDestroy()
    {
        redCheck.OnRedCheckFinishRotate -= RedCheck_OnFinishRotate;
        redCheck.OnRedCheckFail -= RedCheck_OnFail;

    }
    private void RedCheck_OnFinishRotate(object sender, EventArgs args)
    {
        isDelay = true;
        Hide();
        if (roundCounter >= roundCounterMax)
        {
            isStart = false;
            roundText.gameObject.SetActive(false);

            Dictionary<Status, float> rateDict = new Dictionary<Status, float>();
            rateDict[Status.Perfect] = GetPerfectRate();
            rateDict[Status.Good] = GetGoodRate();
            rateDict[Status.Failure] = GetFailureRate();
            OnGreatCheckFinish?.Invoke(this, new OnGreatCheckFinishEventArgs
            {
                rateDict = rateDict,
                maxRound = roundCounterMax
            });
        }


    }
    private void RedCheck_OnFail(object sender, EventArgs args)
    {
        scoreDict[Status.Failure] += 1;
        alertCheck.AlertSkillCheck("Failure!");
    }
    private float GetPerfectRate()
    {
        return (float)scoreDict[Status.Perfect] / roundCounterMax;
    }
    private float GetGoodRate()
    {
        return (float)scoreDict[Status.Good] / roundCounterMax;
    }
    private float GetFailureRate()
    {
        return (float)scoreDict[Status.Failure] / roundCounterMax;
    }


    private void Update()
    {
        if (!isStart)
        {
            return;
        }

        if (isDelay)
        {
            if (delayTimer < delayTimerMax)
            {
                delayTimer += Time.deltaTime;
            }
            else
            {
                isDelay = false;
                delayTimer = 0f;
                NewRound();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                CheckRange(redCheck.GetAbsAngle());
            }
        }

    }

    public void StartRound(int roundCounterMax)
    {
        this.roundCounterMax = Mathf.Min(roundCounterMax, roundMax);
        roundCounter = 0;
        isStart = true;
        isDelay = true;
        delayTimer = 0f;
        roundText.text = $"{roundCounter}/{this.roundCounterMax}";
        roundText.gameObject.SetActive(true);
        scoreDict[Status.Perfect] = 0;
        scoreDict[Status.Good] = 0;
        scoreDict[Status.Failure] = 0;
    }
    private void NewRound()
    {
        roundCounter++;
        successRange.RandomRotate();
        redCheck.SetStart();
        roundText.text = $"{roundCounter}/{roundCounterMax}";
        Show();
    }
    private void CheckRange(float absAngle)
    {

        if (successRange.IsPerfectCheck(absAngle))
        {
            scoreDict[Status.Perfect] += 1;
            alertCheck.AlertSkillCheck("Perfect!");
        }
        else if (successRange.IsGoodCheck(absAngle))
        {
            scoreDict[Status.Good] += 1;
            alertCheck.AlertSkillCheck("Good!");
        }
        else
        {
            scoreDict[Status.Failure] += 1;
            alertCheck.AlertSkillCheck("Failure!");
        }
        redCheck.SetFinish();
    }

    private void Show()
    {
        redCheck.Show();
        successRange.Show();
        circleImageGameObject.SetActive(true);
        spaceImage.SetActive(true);
    }
    private void Hide()
    {
        redCheck.Hide();
        successRange.Hide();
        circleImageGameObject.SetActive(false);
        spaceImage.SetActive(false);
    }
}
