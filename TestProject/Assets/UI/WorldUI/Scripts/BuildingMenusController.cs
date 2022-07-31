using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class BuildingMenusController : MonoBehaviour, IOpenBuildingMenu
{
    public event Action OnClosed;

    [SerializeField] private BuildNewMenu _buildMenuPrefab;
    [SerializeField] private ActionMenu _actionMenuPrefab;

    Dictionary<Type, BuildingMenu> _buildingMenuMap;
    private BuildingMenu _activeMenu;

    private DiContainer _diContainer;
    private ClickInput _clickInput;

    [Inject]
    private void Construct(DiContainer diContainer, ClickInput clickInput)
    {
        _diContainer = diContainer;
        _clickInput = clickInput;
    }
    
    private void Awake()
    {
        _buildingMenuMap = new Dictionary<Type, BuildingMenu>();

        BuildNewMenu buildMenu = _diContainer.InstantiatePrefabForComponent<BuildNewMenu>(_buildMenuPrefab);
        buildMenu.Initialize(this);
        _buildingMenuMap.Add(typeof(BuildNewMenu), buildMenu);

        ActionMenu actionMenu = _diContainer.InstantiatePrefabForComponent<ActionMenu>(_actionMenuPrefab);
        actionMenu.Initialize(this);
        _buildingMenuMap.Add(typeof(ActionMenu), actionMenu);

        _clickInput.OnMouseRightClicked += ForceCloseIfActive;
    }

    private void OnDestroy()
    {
        _clickInput.OnMouseRightClicked -= ForceCloseIfActive;
        if (_activeMenu) _activeMenu.OnMenuClosed -= FinishControl; 
    }
    
    public void OpenBuildingMenuFor(IBuildPoint buildPoint)
    {
        ForceCloseIfActive();
        _activeMenu = buildPoint.IsFree ? GetBuildingMenu<BuildNewMenu>() : GetBuildingMenu<ActionMenu>();
        _activeMenu.transform.localPosition = buildPoint.PointTransform.localPosition;
        _activeMenu.OpenMenu(buildPoint);
        _activeMenu.OnMenuClosed += FinishControl;
    }

    private void FinishControl()
    {
        _activeMenu.OnMenuClosed -= FinishControl;
        _activeMenu = null;
        OnClosed?.Invoke();
    }

    private void ForceCloseIfActive()
    {
        if (_activeMenu)
        {
            _activeMenu.ForceCloseMenu();
        }
    }    

    private BuildingMenu GetBuildingMenu<T>() where T : BuildingMenu
    {
        var type = typeof(T);
        return _buildingMenuMap[type];
    }
}
