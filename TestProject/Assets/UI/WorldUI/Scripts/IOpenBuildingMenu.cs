using System;

public interface IOpenBuildingMenu 
{
    public event Action OnClosed;
    public void OpenBuildingMenuFor(IBuildPoint buildPoint);
}
