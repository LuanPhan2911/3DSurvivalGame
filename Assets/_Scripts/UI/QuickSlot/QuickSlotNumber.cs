using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotNumber : MonoBehaviour
{
    [SerializeField] private int numberIndex;

    [SerializeField] private Color defaultColor, selectedColor;

    [SerializeField] private TextMeshProUGUI numberText;



    private void Awake()
    {
        numberText.text = numberIndex.ToString();
        numberText.color = defaultColor;
    }




    public void SetSelected()
    {
        numberText.color = selectedColor;
    }
    public void SetUnselected()
    {
        numberText.color = defaultColor;
    }
    public int GetNumberIndex()
    {
        return numberIndex;
    }
}
