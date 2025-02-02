using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    [SerializeField]
    private PlayerStatusBar hpStatusBar, staminaStatusBar,
     foodStatusBar, waterStatusBar, weightStatusBar;



    private float hp, maxHp = 100f;
    private float stamina, maxStamina = 100f;
    private float food, maxFood = 100f;
    private float water, maxWater = 100f;
    private float weight, maxWeight = 100f;



    private void Start()
    {
        ResetStatus();
        InvokeRepeating(nameof(AutoDecreaseFoodBar), 60f, 60f);
        InvokeRepeating(nameof(AutoDecreaseWaterBar), 30f, 30f);
    }
    private void OnDestroy()
    {
        CancelInvoke();
    }
    public void ResetStatus()
    {
        hp = maxHp;
        stamina = maxStamina;
        food = maxFood;
        water = maxWater;
        weight = 0f;
        hpStatusBar.SetStatusBar(hp / maxHp);
        staminaStatusBar.SetStatusBar(stamina / maxStamina);
        foodStatusBar.SetStatusBar(food / maxFood);
        waterStatusBar.SetStatusBar(water / maxWater);
        weightStatusBar.SetStatusBar(weight / maxWeight);
    }
    public void SetHp(float changedValue)
    {

        hp = Mathf.Clamp(hp + changedValue, 0f, maxHp);
        hpStatusBar.SetStatusBar(hp / maxHp);
    }
    public void SetStamina(float changedValue)
    {

        stamina = Mathf.Clamp(stamina + changedValue, 0f, maxStamina);
        staminaStatusBar.SetStatusBar(stamina / maxStamina);
    }
    public void SetFood(float changedValue)
    {

        food = Mathf.Clamp(food + changedValue, 0f, maxFood);

        foodStatusBar.SetStatusBar(food / maxFood);
    }
    public void SetWater(float changedValue)
    {

        water = Mathf.Clamp(water + changedValue, 0f, maxWater);

        waterStatusBar.SetStatusBar(water / maxWater);
    }
    public void SetWeight(float changedValue)
    {

        weight = Mathf.Clamp(weight + changedValue, 0f, maxWeight);
        weightStatusBar.SetStatusBar(weight / maxWeight);
    }
    private void AutoDecreaseFoodBar()
    {
        if (food == 0)
        {
            SetHp(-5f);
            return;
        }
        SetFood(-5f);
    }
    private void AutoDecreaseWaterBar()
    {
        if (water == 0)
        {
            SetHp(-5f);
            return;
        }
        SetWater(-5f);
    }
    public bool CanSprint()
    {
        return stamina > 0f;
    }
    public bool IsStaminaFull()
    {
        return stamina == maxStamina;
    }
    public bool IsOverWeight()
    {
        return (weight / maxWeight) > 0.8f;
    }
}
