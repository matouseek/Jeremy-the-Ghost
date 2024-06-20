using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private GameObject _nameInput;
    [SerializeField] private GameObject _leaderboard;
    [SerializeField] private TMP_Dropdown _levelSelectionDropdown;
    [SerializeField] private TMP_Dropdown _sectionSelectionDropdown;
    
    // All the columns of the leaderboard
    [SerializeField] private List<TextMeshProUGUI> _ranks;
    [SerializeField] private List<TextMeshProUGUI> _names;
    [SerializeField] private List<TextMeshProUGUI> _movesToFinish;
    [SerializeField] private List<TextMeshProUGUI> _dates;
    
    // Middle of the screen text for loading msg and error msgs
    [SerializeField] private TextMeshProUGUI _middleScreenText;

    /// <summary>
    /// Fetches the data of the wanted level section and fills the leaderboard with them. 
    /// </summary>
    public void GetLeaderboard(LevelSectionDescription sectionDescription)
    {
        _middleScreenText.text = "Loading";
        _middleScreenText.enabled = true;
        LeaderboardCreator.GetLeaderboard(sectionDescription.LeaderboardPublicKey, 
            entries =>
        {
            _middleScreenText.enabled = false;
            int loopLength = entries.Length < _names.Count ? entries.Length : _names.Count;
            for (int i = 0; i < loopLength; ++i)
            {
                _ranks[i].text = entries[i].Rank.ToString();
                _names[i].text = entries[i].Username;
                _movesToFinish[i].text = entries[i].Score.ToString();
                
                // Get the date in correct format
                var entryDate = DateTimeOffset.FromUnixTimeSeconds((long)entries[i].Date)
                    .ToLocalTime()
                    .Date;
                _dates[i].text = $"{entryDate.Day}.{entryDate.Month}.{entryDate.Year}";
            }
        },
        _ =>
        {
            _middleScreenText.enabled = true;
            _middleScreenText.text = "Error loading leaderboard";
        });
    }

    /// <summary>
    /// Sets an entry into the leaderboard of a level section.
    /// </summary>
    public static void SetEntry(LevelSectionDescription sectionDescription, string playerName, int movesToFinish)
    {
            LeaderboardCreator.UploadNewEntry(sectionDescription.LeaderboardPublicKey, playerName, movesToFinish);
    }
    
    /// <summary>
    /// Displays the leaderboard based on if the player chose a name yet.
    /// </summary>
    public void ShowLeaderboard()
    {
        // The first time a player visits the leaderboard
        // he is tasked with selecting a name
        if (!PlayerPrefs.HasKey("Name"))
        {
            _nameInput.SetActive(true);
            return;
        }
        
        _leaderboard.SetActive(true);
        SelectLevel();
    }
    
    /// <summary>
    /// OnClick function for Submit button in Leaderboard
    /// </summary>
    public void SubmitName()
    {
        PlayerPrefs.SetString("Name", _nameInput.GetComponentInChildren<TMP_InputField>().text);
        _nameInput.SetActive(false);
        ShowLeaderboard();
    }
    
    /// <summary>
    /// OnClick function for Level selection dropdown in Leaderboard
    /// </summary>
    public void SelectLevel()
    {
        _sectionSelectionDropdown.ClearOptions();
        foreach (var section in LevelManager.Instance.Levels[_levelSelectionDropdown.value].LevelSections)
        {
            _sectionSelectionDropdown.options.Add(new TMP_Dropdown.OptionData(section.Name));
        }
        _sectionSelectionDropdown.RefreshShownValue();
        SelectLevelSection();
    }

    public void SelectLevelSection()
    {
        GetLeaderboard(LevelManager
            .Instance
            .Levels[_levelSelectionDropdown.value]
            .LevelSections[_sectionSelectionDropdown.value]);
    }
}
