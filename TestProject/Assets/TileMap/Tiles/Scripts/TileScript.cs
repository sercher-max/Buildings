using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TileScript : MonoBehaviour, ISelectable, IBuildPoint
{
    public int Id { get; private set; }

    // Selectable Block //
    public event Action<ISelectable> OnCursorEnter;
    public event Action<ISelectable> OnCursorExit;

    [SerializeField] private Color _cursorColor;

    private SpriteRenderer _spriteRenderer;
    private Color _myColor;

    // BuildPoint Block //
    public bool IsFree => _myBuildingParameters == null;
    public Transform PointTransform => transform;

    [SerializeField] private Vector3 _buildingOffset;     

    private BuildingParameters _myBuildingParameters;
    private Building _myBuilding;

    public void Initialize(int newId)
    {
        Id = newId;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _myColor = _spriteRenderer.color;
        _myBuildingParameters = null;
    }

    private void OnMouseEnter() => OnCursorEnter?.Invoke(this);
    private void OnMouseExit() => OnCursorExit?.Invoke(this);

    public void SelectableOn() => _spriteRenderer.color = _cursorColor;
    public void SelectableOff() => _spriteRenderer.color = _myColor;


    public void BuildNewBuilding(BuildingParameters buildingParameters)
    {
        if (!IsFree) DestroyBuilding();

        _myBuildingParameters = buildingParameters;      
        _myBuilding = Instantiate(buildingParameters.Prefab, transform);
        _myBuilding.transform.position += _buildingOffset;
        _myBuilding.Initialize();
    }

    public bool TryTakeBuildingParameters(out BuildingParameters buildingParameters)
    {
        buildingParameters = _myBuildingParameters;
        return !IsFree;
    }

    public void DestroyBuilding()
    {
        _myBuilding.Destroy();
        _myBuildingParameters = null;
    }
}
