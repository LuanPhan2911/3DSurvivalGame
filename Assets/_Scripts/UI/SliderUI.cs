using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI amountText;

    [SerializeField] private float minValue = 1;
    [SerializeField] private float defaultValue = 1;
    [SerializeField] private bool isShowAmountText = true;
    [SerializeField] private bool isWholeNumber = true;



    private void Awake()
    {

        slider.minValue = minValue;
        slider.value = defaultValue;
        slider.wholeNumbers = isWholeNumber;
    }

    private void Start()
    {
        slider.onValueChanged.AddListener((value) =>
        {
            slider.value = value;
            UpdateAmountText(value);
        });
        amountText.gameObject.SetActive(isShowAmountText);
    }
    public Slider GetSlider()
    {
        return slider;
    }
    public void ResetSlider()
    {
        slider.value = defaultValue;
    }

    public float GetSliderValue()
    {
        return slider.value;
    }

    public void SetSliderMaxValue(int sliderMaxValue)
    {
        slider.maxValue = sliderMaxValue;
    }
    private void UpdateAmountText(float amount)
    {
        amountText.text = amount.ToString();
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
