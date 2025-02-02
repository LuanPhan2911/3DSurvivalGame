using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusBar : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image filledImage;
    [SerializeField] private Animator animator;
    [SerializeField] private Color warningColor;
    [SerializeField] private Color dangerColor;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private bool isReversed = false;

    private const string IS_FLASH = "IsFlash";

    private void Awake()
    {
        backgroundImage.sprite = defaultSprite;
        filledImage.sprite = defaultSprite;
    }

    private Color GetFilledBarColor(float fillValue)
    {
        Color color = defaultColor;
        if (isReversed)
        {
            if (fillValue >= 0.75f)
            {
                color = dangerColor;
            }
            else if (fillValue > 0.5f && fillValue < 0.75f)
            {
                color = warningColor;
            }
        }
        else
        {
            if (fillValue <= 0.25)
            {
                color = dangerColor;
            }
            else if (fillValue > 0.25f && fillValue < 0.5f)
            {
                color = warningColor;
            }
        }
        return color;
    }

    public void SetStatusBar(float fillValue)
    {


        filledImage.fillAmount = fillValue;
        filledImage.color = GetFilledBarColor(fillValue);

        animator.SetTrigger(IS_FLASH);
    }



}
