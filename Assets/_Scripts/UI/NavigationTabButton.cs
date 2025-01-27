using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationTabButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject selectedGameObject;
    [SerializeField] private GameObject tabContentGameObject;


    public Button GetButton()
    {
        return button;
    }
    private void Awake()
    {
        HideSelectedGameObject();
    }


    public void ShowSelectedGameObject()
    {
        selectedGameObject.SetActive(true);
    }
    public void HideSelectedGameObject()
    {
        selectedGameObject.SetActive(false);
    }
    public void ShowTabContent()
    {
        tabContentGameObject.SetActive(true);
    }
    public void HideTabContent()
    {
        tabContentGameObject.SetActive(false);
    }
}
