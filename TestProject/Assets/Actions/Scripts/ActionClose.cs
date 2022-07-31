using UnityEngine;

public class ActionClose : IBuildingAction
{
    public Sprite ActionIcon { get { return _icon ? _icon : _icon = Resources.Load<Sprite>(ACTION_CLOSE_ICON_PATH); } }

    public bool IsDone { get; private set; }

    private const string ACTION_CLOSE_ICON_PATH = "Actions/ActionCloseIcon";
    private Sprite _icon;

    public void MakeAction(IBuildPoint buildPoint) => IsDone = true;
    public void ForceInterrupt() => IsDone = true;
}
