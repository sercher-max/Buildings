using System;
using UnityEngine;

public class ClickInput : MonoBehaviour
{
    public event Action<int> OnMouseClicked;
    public event Action OnMouseLeftClicked;
    public event Action OnMouseLeftUp;
    public event Action OnMouseRightClicked;
    public event Action OnMouseRightUp;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClicked?.Invoke(0);
            OnMouseLeftClicked?.Invoke();
        }       
        if (Input.GetMouseButtonDown(1))
        {
            OnMouseClicked?.Invoke(1);
            OnMouseRightClicked?.Invoke();
        }

        if (Input.GetMouseButtonUp(0)) OnMouseLeftUp.Invoke();
        if (Input.GetMouseButtonUp(1)) OnMouseRightUp?.Invoke();
    }
}

