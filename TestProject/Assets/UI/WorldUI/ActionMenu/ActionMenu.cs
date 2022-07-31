using System.Collections.Generic;
using Zenject;

public class ActionMenu : BuildingMenu
{
    private DescriptionPanel _descriptionPanel;
    private RayCastInput _rayCastInput;

    [Inject]
    private void Construct(DescriptionPanel descriptionPanel, RayCastInput rayCastInput)
    {
        _descriptionPanel = descriptionPanel;
        _rayCastInput = rayCastInput;
    }
      
    public override void Initialize(BuildingMenusController buildingController)
    {
        _buttons = new List<ButtonScript>();
        _actions = new List<IBuildingAction>();        

        _actions.Add(new ActionClose());
        _actions.Add(new ActionMove(_buttonPrefab, transform, _rayCastInput));
        _actions.Add(new ActionDestroy());
        
        for (int i = 0; i < _actions.Count; i++)
        {
            ButtonScript temp = _diContainer.InstantiatePrefabForComponent<ButtonScript>(_buttonPrefab, transform);
            temp.Initialize(i, _actions[i].ActionIcon, this);
            _buttons.Add(temp);
        }

        _buildingController = buildingController;
        _circlePositions = CircleLayout.GetCirclePositions(_buttons.Count, _menuRadius, _radiusMod);
        CloseMenu();
    }

    public override void OpenMenu(IBuildPoint buildPoint)
    {
        if (buildPoint.TryTakeBuildingParameters(out BuildingParameters buildingParameters))
            _descriptionPanel.SwitchOnPanel(buildingParameters, true);

        base.OpenMenu(buildPoint);
    }

    protected override void CloseMenu()
    {
        _descriptionPanel.SwitchOffPanel(false);
        base.CloseMenu();
    }
}
