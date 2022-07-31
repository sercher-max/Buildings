using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class RayCastInput : MonoBehaviour
{
    [SerializeField] private bool _waitForUp = false;

    public event Action<RaycastHit2D> OnHit;
    private RaycastHit2D _tempHit;

    private ClickInput _clickInput;

    [Inject]
    private void Construct(ClickInput clickInput)
    {
        _clickInput = clickInput;
    }

    private void Awake()
    {
        _clickInput.OnMouseLeftClicked += OnClick;
        _clickInput.OnMouseLeftUp += OnUp;
    }

    private void OnDestroy()
    {
        _clickInput.OnMouseLeftClicked -= OnClick;
        _clickInput.OnMouseLeftUp -= OnUp;
    }

    private void OnClick()
    {
        _tempHit = FindHit();

        if (_waitForUp) return;

        if (!EventSystem.current.IsPointerOverGameObject() && _tempHit)
        {
            OnHit?.Invoke(_tempHit);
        }
    }

    private void OnUp()
    {
        if (!_waitForUp) return;

        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (_tempHit && _tempHit.collider == FindHit().collider) OnHit?.Invoke(_tempHit);
    }

    private RaycastHit2D FindHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        
        return hit;        
    }
}
