using UnityEngine;

public class ActionMove : IBuildingAction, IButtonListener
{
    private const string ACTION_MOVE_ICON_PATH = "Actions/ActionMoveIcon";
    private Sprite _icon;
    public Sprite ActionIcon { get { return _icon ? _icon : _icon = Resources.Load<Sprite>(ACTION_MOVE_ICON_PATH); } }

    public bool IsDone { get; private set; }

    private RayCastInput _rayCastInput;
    private IBuildPoint _buildPoint;
    private ButtonScript _closeButton;
    private IActionIcon _closeIcon;

    public ActionMove(ButtonScript buttonPrefab, Transform parent, RayCastInput rayCastInput)
    {
        _rayCastInput = rayCastInput;
        _closeIcon = new ActionClose();
        _closeButton = GameObject.Instantiate(buttonPrefab, parent);
        _closeButton.Initialize(0, _closeIcon.ActionIcon, this);
        _closeButton.gameObject.SetActive(false);
    }

    public void MakeAction(IBuildPoint buildPoint)
    {
        IsDone = false;
        _buildPoint = buildPoint;

        _closeButton.gameObject.SetActive(true);
        _closeButton.transform.localPosition = Vector2.zero;

        _rayCastInput.OnHit += OnHit;
    }

    private void OnHit(RaycastHit2D hit)
    {
        if (hit.transform.TryGetComponent(out IBuildPoint buildPoint))
        {
            if (!buildPoint.IsFree) return;

            if (_buildPoint.TryTakeBuildingParameters(out BuildingParameters buildingParameters))
            {
                _buildPoint.DestroyBuilding();
                buildPoint.BuildNewBuilding(buildingParameters);
                Done();
            }           
        }        
    }

    public void ForceInterrupt() => Done();

    private void Done()
    {
        _closeButton.gameObject.SetActive(false);
        _rayCastInput.OnHit -= OnHit;
        IsDone = true;
    }

    public void OnButtonClick(int index) => ForceInterrupt();
}
