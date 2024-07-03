using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Handles all actions done in the menu.
/// </summary>
public class MenuManager : MonoBehaviour
{
    
    // ---------- All the menus ----------
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _playMenu;
    [SerializeField] private GameObject _leaderboardMenu;
    [SerializeField] private GameObject _customizationMenu;
    private GameObject _currentMenu;

    // ---------- Level selection ----------
    [SerializeField] private List<GameObject> _levelSelectionButtons;
    [SerializeField] private GameObject _nameInput;

    // This is set from the JeremyController after finishing a level
    // so that the player is loaded immediately into the level selection
    // and not into the main menu
    public static bool ShowPlayMenuOnLoad = false;
    
    // ---------- Leaderboard ----------
    [SerializeField] private LeaderboardManager _leaderboardManager;

    private void Awake()
    {
        _currentMenu = _mainMenu;
        if(ShowPlayMenuOnLoad) Play(); // Do the same thing as clicking on the Play button
    }

    /// <summary>
    /// OnClick function for Play button in MainMenu
    /// </summary>
    public void Play()
    {
        _mainMenu.SetActive(false);
        
        // The first time a player visits the level selection
        // he is tasked with selecting a name
        if (!PlayerPrefs.HasKey("Name"))
        {
            _currentMenu = _nameInput;
            _nameInput.SetActive(true);
            return;
        }
        _currentMenu = _playMenu;
        _playMenu.SetActive(true);
        ShowPlayableLevels();
    }

    /// <summary>
    /// OnClick function for Quit button in MainMenu
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// OnClick function for Return button in PlayMenu and Leaderboard
    /// </summary>
    public void ReturnToMainMenu()
    {
        _currentMenu.SetActive(false);
        _mainMenu.SetActive(true);
        _currentMenu = _mainMenu;
    }

    /// <summary>
    /// Shows all the levels that are marked as completed in their scriptable object.
    /// </summary>
    private void ShowPlayableLevels()
    {
        // The level at index 0 is always shown
        for(int i = 1; i < LevelManager.Instance.Levels.Count; ++i)
        {
            if (!LevelManager.Instance.Levels[i-1].Completed) continue;
            _levelSelectionButtons[i].SetActive(true);
        }
    }

    /// <summary>
    /// OnClick function for Leaderboard button in MainMenu
    /// </summary>
    public void ShowLeaderboard()
    {
        _mainMenu.SetActive(false);
        _leaderboardMenu.SetActive(true);
        _currentMenu = _leaderboardMenu;
        
        _leaderboardManager.ShowLeaderboard();
    }

    /// <summary>
    /// OnClick function for Customization button in MainMenu
    /// </summary>
    public void ShowCustomization()
    {
        _mainMenu.SetActive(false);
        _customizationMenu.SetActive(true);
        _currentMenu = _customizationMenu;
    }
    
    /// <summary>
    /// OnClick function for Submit button in PlayMenu
    /// </summary>
    public void SubmitName()
    {
        PlayerPrefs.SetString("Name", _nameInput.GetComponentInChildren<TMP_InputField>().text);
        _nameInput.SetActive(false);
        Play(); // Show play menu
    }

    /// <summary>
    /// Debugging function to delete all player pref keys.
    /// </summary>
    public void DeleteAllKeys()
    {
        PlayerPrefs.DeleteAll();
    }
}
