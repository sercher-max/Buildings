using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private BuildingMenusController _buildingController;
    [SerializeField] private DescriptionPanel _descriptionPanel;
    [SerializeField] private ClickInput _clickInput;
    [SerializeField] private RayCastInput _rayCastInput;

    public override void InstallBindings()
    {
        BindBuildingMenusController();
        BindFromInstance(_descriptionPanel);
        BindFromInstance(_clickInput);
        BindFromInstance(_rayCastInput);
    }

    private void BindBuildingMenusController()
    {
        Container.Bind<IOpenBuildingMenu>()
           .To<BuildingMenusController>()
           .FromInstance(_buildingController)
           .AsSingle();
    }

    private void BindFromInstance<T>(T bindObj)
    {
        Container.Bind<T>()
           .FromInstance(bindObj)
           .AsSingle();
    }
}
