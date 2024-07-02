using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: document what this class is doing, mainly the eyes swapping
//because it may seem confusing as to whats going on
public class CustomizationManager : MonoBehaviour
{
    [SerializeField] private Image _jeremyUIImage;
    [SerializeField] private FlexibleColorPicker _fcp;
    [SerializeField] private JeremyDescription _jeremyDescription;
    [SerializeField] private List<Sprite> _eyes; //todo: deserialize
    [SerializeField] private int _selectedEyesIndex; //todo: deserialize
    [SerializeField] private Image _eyesUIImage;

    private void Start()
    {
        LoadCollectedEyes();
        _fcp.color = _jeremyDescription.Color;
        _selectedEyesIndex = _jeremyDescription.Eyes == null ? 0 : _eyes.IndexOf(_jeremyDescription.Eyes);
        ShowEyes();
    }

    /// <summary>
    /// Called when color changes in the color picker in the Customization menu.
    /// </summary>
    public void OnColorChange()
    {
        _jeremyUIImage.color = _fcp.color;
        _jeremyDescription.Color = _fcp.color; // Save color change
    }

    private void LoadCollectedEyes()
    {
        _eyes.Add(null); // The first option is no sprite
        for(int i = 1; i < LevelManager.Instance.Levels.Count; ++i)
        {
            if (!LevelManager.Instance.Levels[i].Completed) continue;
            _eyes.Add(Resources.Load<Sprite>($"eyes_{i}"));
        }
    }

    private void ShowEyes()
    {
        _eyesUIImage.color = new(_eyesUIImage.color.r
            , _eyesUIImage.color.g
            , _eyesUIImage.color.b
            , _selectedEyesIndex == 0 ? 0 : 1); // If we don't have any sprite selected (index == 0)
                                                  // then we turn alpha down to 0
        _eyesUIImage.sprite = _eyes[_selectedEyesIndex];
    }
}
