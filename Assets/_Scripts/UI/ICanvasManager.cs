using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanvasManager
{
    public event EventHandler OnUIOpenChanged;
    public bool GetIsOpen();
    public void Show();
    public void Hide();
    public void ToggleIsOpen();


}
