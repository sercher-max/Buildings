using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable 
{
    public event Action<ISelectable> OnCursorEnter;
    public event Action<ISelectable> OnCursorExit;

    void SelectableOn();
    void SelectableOff();
}
