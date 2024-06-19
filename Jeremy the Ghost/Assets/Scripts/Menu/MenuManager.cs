using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Handles all actions done in the menu.
/// </summary>
public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _playMenu;
    [SerializeField] private GameObject _leaderboardMenu;
    private GameObject _currentMenu;

    private static readonly string[] _levelNames = { "IntroLevel", "TheFactory" };
    [SerializeField] private GameObject[] _levelsButtons = new GameObject[_levelNames.Length];
    private static int _currentlyPlayedLevelIndex = -1;

    // This is set from the JeremyController after finishing a level
    // so that the player is loaded immediately into the level selection
    // and not into the main menu
    public static bool ShowPlayMenuOnLoad = false;
    
    [SerializeField] private GameObject _nameInput;

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
        _playMenu.SetActive(true);
        _currentMenu = _playMenu;
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
    /// OnClick function for Intro Level button in PlayMenu
    /// </summary>
    public void LoadIntroLevel()
    {
        LoadLevel(0);
    }

    /// <summary>
    /// OnClick function for The Factory button in PlayMenu
    /// </summary>
    public void LoadTheFactoryLevel()
    {
        LoadLevel(1);
    }
    
    private void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(_levelNames[levelIndex]);
        _currentlyPlayedLevelIndex = levelIndex;
    }

    /// <summary>
    /// Marks the next level in the game as playable to the player. This is done by setting
    /// a value 1 to the level name in player preferences.
    /// </summary>
    public static void CompleteLevel()
    {
        if (_currentlyPlayedLevelIndex == _levelNames.Length - 1) return; // No new level will show after the last one
        PlayerPrefs.SetInt(_levelNames[_currentlyPlayedLevelIndex + 1], 1);
    }

    /// <summary>
    /// Shows all the levels that have a value of 1 in player preferences (i.e. playable levels).
    /// </summary>
    private void ShowPlayableLevels()
    {
        for(int i = 0; i < _levelNames.Length; ++i)
        {
            if (PlayerPrefs.GetInt(_levelNames[i]) != 1) continue;
            _levelsButtons[i].SetActive(true);
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
        
        // The first time a player visits the leaderboard
        // he is tasked with selecting a name
        if (!PlayerPrefs.HasKey("Name"))
        {
            _nameInput.SetActive(true);
            return;
        }
        //TODO show leaderboard
    }

    /// <summary>
    /// OnClick function for Submit button in Leaderboard
    /// </summary>
    public void SubmitName()
    {
        PlayerPrefs.SetString("Name", _nameInput.GetComponentInChildren<TMP_InputField>().text);
        _nameInput.SetActive(false);
    }

    /// <summary>
    /// Debugging function to delete all player pref keys.
    /// </summary>
    public void DeleteAllKeys()
    {
        PlayerPrefs.DeleteAll();
    }
}
