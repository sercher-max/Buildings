using UnityEngine;

public class ActionBuild : IBuildingAction, IBuildingParametersContainer
{
    public BuildingParameters Parameters { get; }
    public Sprite ActionIcon => Parameters.Icon;
    public bool IsDone { get; private set; }

    public ActionBuild(BuildingParameters buildingParameters) => Parameters = buildingParameters;

    public void MakeAction(IBuildPoint buildPoint)
    {
        buildPoint.BuildNewBuilding(Parameters);
        IsDone = true;
    }

    public void ForceInterrupt() => IsDone = true;
}
