using UnityEngine;
using Zenject;

public class TileMapManager : MonoBehaviour
{
    [SerializeField] private TileMapCreateParameters _tileMapCreateParameters;

    private TileMapSelectableController _selectableController;
    private TileMapClickController _tileClickController;

    private IOpenBuildingMenu _openBuildingMenu;
    private RayCastInput _rayCastInput;

    private TileScript[] _tiles;   

    [Inject]
    private void Construct(IOpenBuildingMenu openBuildingMenu, RayCastInput rayCastInput)
    {
        _openBuildingMenu = openBuildingMenu;
        _rayCastInput = rayCastInput;
    }

    private void Awake()
    {
        _tiles = TileCreator.CreateTileMap(_tileMapCreateParameters, transform);

        _selectableController = new TileMapSelectableController(_tiles);
        _tileClickController = new TileMapClickController(_selectableController, _openBuildingMenu, _rayCastInput);
    }
    
    private void OnDestroy()
    {
        _selectableController.Destroy();
        _tileClickController.Destroy();
    }
}
  