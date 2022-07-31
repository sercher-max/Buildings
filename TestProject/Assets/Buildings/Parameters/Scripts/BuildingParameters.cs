using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Buildings/Parameters", fileName = "BuildingParameters")]
public class BuildingParameters : ScriptableObject
{ 
    [Header("Parameters")]
    [SerializeField] private Building _buildingPrefab;

    [Space]

    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private string _type;
    [TextArea(3,8)]
    [SerializeField] private string _description;


    public Building Prefab     { get { return _buildingPrefab; } }
    public Sprite Icon         { get { return _icon; } }
    public string Name         { get { return _name; } }
    public string Type         { get { return _type; } }
    public string Description  { get { return _description; } }
}
