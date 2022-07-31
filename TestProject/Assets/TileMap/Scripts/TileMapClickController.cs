using UnityEngine;

public class TileMapClickController 
{
    private bool _waitForBuildingController;

    private TileMapSelectableController _tileSelectableController;
    private IOpenBuildingMenu _openBuildingMenu;
    private RayCastInput _rayCastInput;

    public TileMapClickController(TileMapSelectableController tileSelectableController, IOpenBuildingMenu openBuildingMenu, RayCastInput rayCastInput)
    {
        _tileSelectableController = tileSelectableController;
        _openBuildingMenu = openBuildingMenu;

        _rayCastInput = rayCastInput;
        _rayCastInput.OnHit += OnHit;      
    }

    public void Destroy()
    {
        _rayCastInput.OnHit -= OnHit;       
        _openBuildingMenu.OnClosed -= OnBuildingControlFinished;
    }

    private void OnHit(RaycastHit2D hit)
    {
        if (_waitForBuildingController) return;
        
        if (hit.collider.TryGetComponent(out IBuildPoint buildPoint))
        {
            _openBuildingMenu.OpenBuildingMenuFor(buildPoint);
            _waitForBuildingController = true;
            _openBuildingMenu.OnClosed += OnBuildingControlFinished;

            if (hit.collider.TryGetComponent(out TileScript selectable))
                _tileSelectableController.LockThisSelectable(selectable);
        }
    }

    private void OnBuildingControlFinished()
    {
        _openBuildingMenu.OnClosed -= OnBuildingControlFinished;
        _waitForBuildingController = false;
        _tileSelectableController.UnlockSelectable();        
    }
}
