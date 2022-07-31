using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildPoint 
{
    public bool IsFree { get; }
    public Transform PointTransform { get; }
    public void BuildNewBuilding(BuildingParameters buildingParameters);
    public bool TryTakeBuildingParameters(out BuildingParameters buildingParameters);
    public void DestroyBuilding();
}
