using System.Collections.Generic;

public class TileMapSelectableController 
{   
    public bool IsCursorTile { get { return _cursorSelectable != null; } }

    private ISelectable LockedSelectable;
    private ISelectable _cursorSelectable;
    private List<ISelectable> _selectables = new List<ISelectable>();

    public TileMapSelectableController() { }
    public TileMapSelectableController(ISelectable[] selectables) => AddSelectableTiles(selectables);

    public void AddSelectableTiles(ISelectable[] selectables)
    {
        for (int i = 0; i < selectables.Length; i++)
        {
            selectables[i].OnCursorEnter += CursorEnter;
            selectables[i].OnCursorExit += CursorExit;
        }
        _selectables.AddRange(selectables);
    }

    public void Destroy()
    {
        for (int i = 0; i < _selectables.Count; i++)
        {
            _selectables[i].OnCursorEnter -= CursorEnter;
            _selectables[i].OnCursorExit -= CursorExit;
        }
    }

    public void LockThisSelectable(TileScript selectable)
    {
        if (LockedSelectable != null) LockedSelectable.SelectableOff();

        LockedSelectable = selectable;
        LockedSelectable.SelectableOn();
    }

    public void UnlockSelectable()
    {
        LockedSelectable.SelectableOff();
        LockedSelectable = null;
    }

    private void CursorEnter(ISelectable selectable)
    {
        _cursorSelectable = selectable;
        if (LockedSelectable != selectable) selectable.SelectableOn();
    }

    private void CursorExit(ISelectable selectable)
    {
        _cursorSelectable = null;
        if (LockedSelectable != selectable) selectable.SelectableOff();
    }
}
