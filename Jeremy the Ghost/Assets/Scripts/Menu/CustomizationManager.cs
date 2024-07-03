using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the actions in customization menu.
/// </summary>
public class CustomizationManager : MonoBehaviour
{
    [SerializeField] private Image _jeremyUIImage;
    [SerializeField] private FlexibleColorPicker _fcp;
    [SerializeField] private JeremyDescription _jeremyDescription;
    [SerializeField] private List<Sprite> _collectedEyes; // All eyes that the player collected
                                         // at index 0 is a null value resembling no eye sprite
    private int _selectedEyesIndex; // Currently selected eyes
    [SerializeField] private Image _eyesUIImage; // Image of currently selected eyes

    [SerializeField] private GameObject _eyeChangeButtonRight;
    [SerializeField] private GameObject _eyeChangeButtonLeft;

    // Due to the order of calls OnColorChange, Awake and OnEnable
    // (OnColorChange -> Awake -> OnColorChange -> OnEnable)
    // we have to prevent changing _jeremyUIImage color
    // until the data from _jeremyDescription is loaded
    private bool _changeColor = false;

    // _changeColor needs to be set here due to the fact that
    // Awake() is called once per script (that will create a bug
    // if we open CustomizationMenu, close it and reopen it)
    private void OnEnable()
    {
        _changeColor = true;
        OnColorChange(_fcp.color);
    }

    private void OnDisable()
    {
        _changeColor = false;
    }

    private void Awake()
    {
        LoadCollectedEyes();
        
        if (_collectedEyes.Count > 1) // At least one eye skin other than default (null) is acquired
        { ShowEyeChangeButtons(); }
        
        // Set the correct data to the color picker + eye selector
        _fcp.color = _jeremyDescription.Color;
        _selectedEyesIndex = _jeremyDescription.Eyes == null ? 0 : _collectedEyes.IndexOf(_jeremyDescription.Eyes);
        
        ShowEyes();
    }

    /// <summary>
    /// Called when color changes in the color picker in the Customization menu.
    /// </summary>
    public void OnColorChange(Color color)
    {
        if (!_changeColor) return;
        _jeremyUIImage.color = color;
        _jeremyDescription.Color = color; // Save color change
    }

    /// <summary>
    /// Loads all the eye sprites the player has collected.
    /// </summary>
    private void LoadCollectedEyes()
    {
        _collectedEyes.Add(null); // The sprite is no sprite - default eyes
        for(int i = 1; i < LevelManager.Instance.Levels.Count; ++i)
        {
            if (!LevelManager.Instance.Levels[i].Completed) continue;
            _collectedEyes.Add(Resources.Load<Sprite>($"eyes_{i}"));
        }
    }

    /// <summary>
    /// Shows the eyes at the _selectedEyesIndex.
    /// </summary>
    private void ShowEyes()
    {
        _eyesUIImage.color = new(_eyesUIImage.color.r
            , _eyesUIImage.color.g
            , _eyesUIImage.color.b
            , _selectedEyesIndex == 0 ? 0 : 1); // If we don't have any sprite selected (index == 0)
                                                  // then we turn alpha down to 0 (otherwise a white block will be shown)
        _eyesUIImage.sprite = _collectedEyes[_selectedEyesIndex];
        _jeremyDescription.Eyes = _collectedEyes[_selectedEyesIndex];
    }

    /// <summary>
    /// Shows the buttons that give you an option to
    /// switch between different eye sprites.
    /// </summary>
    private void ShowEyeChangeButtons()
    {
        _eyeChangeButtonLeft.SetActive(true);
        _eyeChangeButtonRight.SetActive(true);
    }

    /// <summary>
    /// OnClick function for the right eye changing button.
    /// </summary>
    public void ChangeEyesRight()
    {
        _selectedEyesIndex = (++_selectedEyesIndex) % _collectedEyes.Count;
        ShowEyes();
    }

    /// <summary>
    /// OnClick function for the left eye changing button.
    /// </summary>
    public void ChangeEyesLeft()
    {
        if (--_selectedEyesIndex < 0)
        {
            _selectedEyesIndex = _collectedEyes.Count - 1;
        }
        ShowEyes();
    }
}
