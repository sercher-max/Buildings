using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/TileMap/Parameters", fileName = "TileMapParameters")]
public class TileMapCreateParameters : ScriptableObject
{ 
    [Header("Create Parameters")]
    [SerializeField] private TileScript _tilePrefab;

    [Space]

    [SerializeField] private Vector2Int _tileCount;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private bool _fillCenter;


    public TileScript TilePrefab    { get { return _tilePrefab; } }
    public Vector2Int TileCount     { get { return _tileCount; } }
    public Vector2 Offset           { get { return _offset; } }
    public bool FillCenter          { get { return _fillCenter; } }
}
