using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButtonSingle : MonoBehaviour
{

    [SerializeField] private Button categoryButton;
    [SerializeField] private ToolCategoryUI toolCategoryUI;
    [SerializeField] private CraftingSystemUI craftingSystemUI;


    private void Awake()
    {
        categoryButton.onClick.AddListener(() =>
        {

            toolCategoryUI.Show();
            craftingSystemUI.Hide();
        });
    }

}
