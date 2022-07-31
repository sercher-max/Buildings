using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class UIDescPanelDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private DescriptionPanel _descriptionPanel;
    private IBuildingParametersContainer _buildingParametersContainer;
    private bool _isActive;

    [Inject]
    private void Construct(DescriptionPanel descriptionPanel)
    {
        _descriptionPanel = descriptionPanel;
        _isActive = false;
    }

    public void Initialize(IBuildingParametersContainer buildingParametersContainer)
    {
        _buildingParametersContainer = buildingParametersContainer;
        _isActive = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isActive) _descriptionPanel.SwitchOnPanel(_buildingParametersContainer.Parameters);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isActive) _descriptionPanel.SwitchOffPanel();
    }

    private void OnDisable()
    {
        if (_isActive) _descriptionPanel.SwitchOffPanel();
    }
}
