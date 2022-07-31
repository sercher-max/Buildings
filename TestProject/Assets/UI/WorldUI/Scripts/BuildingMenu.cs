using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Canvas))]
public abstract class BuildingMenu : MonoBehaviour, IButtonListener
{
    public event Action OnMenuClosed;

    [Header("Menu Parameters")]
    [SerializeField] protected ButtonScript _buttonPrefab;
    [SerializeField] protected float _menuRadius = 1f;
    [SerializeField] protected float _radiusMod = 0.2f;
    protected Vector3[] _circlePositions;

    [Header("Animation Parameters")]
    [SerializeField] protected float _buttonsSpeed = 1f;
    protected bool _moveCompleted;

    [Inject] protected DiContainer _diContainer;

    protected List<IBuildingAction> _actions;
    protected IBuildingAction _activeAction;

    protected BuildingMenusController _buildingController;
    protected List<ButtonScript> _buttons;
    protected IBuildPoint _buildPoint;

    private void Awake() => GetComponent<Canvas>().worldCamera = Camera.main;

    public abstract void Initialize(BuildingMenusController buildingController);

    public virtual void OpenMenu(IBuildPoint buildPoint)
    {
        _buildPoint = buildPoint;
        _moveCompleted = false;

        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].gameObject.SetActive(true);
            _buttons[i].transform.position = transform.position;
        }
    }

    public void ForceCloseMenu()
    {
        if (_activeAction == null)
        {
            CloseMenu();
            OnMenuClosed?.Invoke();
        }
        else _activeAction.ForceInterrupt();
    }
 
    public async void OnButtonClick(int index)
    {
        _activeAction = _actions[index];
        _activeAction.MakeAction(_buildPoint);
        CloseMenu();

        while (!_actions[index].IsDone)
        {
            await Task.Yield();
        }

        _activeAction = null;
        OnMenuClosed?.Invoke();
    }

    protected virtual void CloseMenu()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].gameObject.SetActive(false);
        }
    }

    private void Update() { if (!_moveCompleted) ButtonMove(); }

    private void ButtonMove()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            Transform temp = _buttons[i].transform;

            if (Vector3.Distance(temp.position, transform.position + _circlePositions[i]) > _buttonsSpeed * Time.deltaTime)
            {
                Vector3 vec = Vector3.MoveTowards(temp.position,
                    transform.position + _circlePositions[i],
                    _buttonsSpeed * Time.deltaTime);

                temp.position = vec;
            }
            else
            {
                for (int j = 0; j < _buttons.Count; j++)
                {
                    temp.position = transform.position + _circlePositions[i];
                    _moveCompleted = true;
                }
            }
        }
    }
}
