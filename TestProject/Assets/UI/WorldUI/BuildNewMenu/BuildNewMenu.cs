using System.Collections.Generic;
using UnityEngine;

public class BuildNewMenu : BuildingMenu
{
    private const string BUILDING_PARAMETERS_PATH = "Buildings/";   
    
    private BuildingParameters[] _buildingParameters;

    public override void Initialize(BuildingMenusController buildingController)
    {
        _buildingParameters = Resources.LoadAll<BuildingParameters>(BUILDING_PARAMETERS_PATH);
        _buttons = new List<ButtonScript>();
        _actions = new List<IBuildingAction>();
        
        AddCloseButton();
        AddBuildButtons();

        _buildingController = buildingController;
        _circlePositions = CircleLayout.GetCirclePositions(_buttons.Count, _menuRadius, _radiusMod);
        CloseMenu();
    }

    private void AddBuildButtons()
    {
        int mod = _buttons.Count;
        for (int i = mod; i < _buildingParameters.Length + mod; i++)
        {
            ActionBuild action = new ActionBuild(_buildingParameters[i-mod]);
            ButtonScript button = _diContainer.InstantiatePrefabForComponent<ButtonScript>(_buttonPrefab, transform);
            button.Initialize(i, action.ActionIcon, this);
            _diContainer.InstantiateComponent<UIDescPanelDetector>(button.gameObject).Initialize(action);
            _buttons.Add(button);
            _actions.Add(action);
        }
    }

    private void AddCloseButton()
    {
        ActionClose action = new ActionClose();
        ButtonScript button = _diContainer.InstantiatePrefabForComponent<ButtonScript>(_buttonPrefab, transform);
        button.Initialize(_buttons.Count, action.ActionIcon, this);
        _buttons.Add(button);
        _actions.Add(action);
    }
}
