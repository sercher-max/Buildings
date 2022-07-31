using UnityEngine;

public class ActionDestroy : IBuildingAction
{
    private const string ACTION_DESTROY_ICON_PATH = "Actions/ActionDestroyIcon";
    private Sprite _icon;
    public Sprite ActionIcon { get { return _icon ? _icon : _icon = Resources.Load<Sprite>(ACTION_DESTROY_ICON_PATH); } }

    public bool IsDone { get; private set; }

    public void MakeAction(IBuildPoint buildPoint)
    {
        buildPoint.DestroyBuilding();
        IsDone = true;
    }

    public void ForceInterrupt() => IsDone = true;
}
