using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanvasManager
{

    public bool GetIsOpen();
    public void Show();
    public void Hide();
    public void ToggleIsOpen();


}
