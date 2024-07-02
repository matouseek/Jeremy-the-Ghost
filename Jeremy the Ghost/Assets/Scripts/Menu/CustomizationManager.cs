using UnityEngine;
using UnityEngine.UI;

public class CustomizationManager : MonoBehaviour
{
    [SerializeField] private Image _jeremyUIImage;
    [SerializeField] private FlexibleColorPicker _fcp;
    [SerializeField] private JeremyDescription _jeremyDescription;

    private void Awake()
    {
        _fcp.color = _jeremyDescription.Color;
    }

    /// <summary>
    /// Called when color changes in the color picker in the Customization menu.
    /// </summary>
    public void OnColorChange()
    {
        _jeremyUIImage.color = _fcp.color;
        _jeremyDescription.Color = _fcp.color; // Save color change
    }
}
