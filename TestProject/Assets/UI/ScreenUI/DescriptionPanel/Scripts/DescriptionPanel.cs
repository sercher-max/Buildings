using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionPanel : MonoBehaviour
{
    [SerializeField] GameObject _objectForSwitchActive;
    [SerializeField] Image _descIcon;
    [SerializeField] Text _descName;
    [SerializeField] Text _descType;
    [SerializeField] Text _descText;

    private bool _isLocked;

    private void Awake()
    {
        _objectForSwitchActive?.SetActive(false);
    }

    public void SwitchOnPanel(BuildingParameters buildingParameters, bool Lock = false)
    {
        if (_isLocked) return;
        _isLocked = Lock;

        _objectForSwitchActive?.SetActive(true);

        _descIcon.sprite = buildingParameters.Icon;
        _descName.text = buildingParameters.Name;
        _descType.text = buildingParameters.Type;
        _descText.text = buildingParameters.Description;
    }

    public void SwitchOffPanel(bool unlock = false)
    {
        _isLocked = unlock;
        if (_isLocked) return;

        if (_objectForSwitchActive) _objectForSwitchActive.SetActive(false);
    }
}
