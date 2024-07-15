using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Dan.Main;

public class LeaderboardManager : MonoBehaviour
{
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
        ClearPreviousEntries();
        _middleScreenText.text = "Loading";
        _middleScreenText.enabled = true;
        LeaderboardCreator.GetLeaderboard(sectionDescription.LeaderboardPublicKey, 
            entries =>
            {
                entries = entries.Reverse().ToArray();
                _middleScreenText.enabled = false;
                int loopLength = entries.Length < _names.Count ? entries.Length : _names.Count;
                for (int i = 0; i < loopLength; ++i)
                {
                    _ranks[i].text = (i+1).ToString();
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

    private void ClearPreviousEntries()
    {
        for (int i = 0; i < _ranks.Count; ++i)
        {
            _ranks[i].text = "";
            _names[i].text = "";
            _movesToFinish[i].text = "";
            _dates[i].text = "";
        }
    }

    /// <summary>
    /// Sets an entry into the leaderboard of a level section.
    /// </summary>
    public static void SetEntry(LevelSectionDescription sectionDescription, int movesToFinish)
    {
        LeaderboardCreator.GetPersonalEntry(sectionDescription.LeaderboardPublicKey, 
            entry =>
            {
                if (entry.Rank == 0 && entry.Score == 0) // Player has not set an entry
                {
                    // Set their first entry
                    LeaderboardCreator.UploadNewEntry(sectionDescription.LeaderboardPublicKey,
                        PlayerPrefs.GetString("Name"), movesToFinish);
                }
                else if (movesToFinish < entry.Score) // Player has beaten their PB
                {
                    // Remove previous entry and set a new one
                    LeaderboardCreator.DeleteEntry(sectionDescription.LeaderboardPublicKey);
                    LeaderboardCreator.UploadNewEntry(sectionDescription.LeaderboardPublicKey,
                        PlayerPrefs.GetString("Name"), movesToFinish);
                }
                
                // If player had an entry and hasn't beaten it, we don't want to do anything
            });
    }
    
    /// <summary>
    /// Displays the leaderboard with the data for currently selected level and level section.
    /// </summary>
    public void ShowLeaderboard()
    {
        _leaderboard.SetActive(true);
        SelectLevel();
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

    /// <summary>
    /// OnClick function for LevelSection selection dropdown in Leaderboard
    /// </summary>
    public void SelectLevelSection()
    {
        GetLeaderboard(LevelManager
            .Instance
            .Levels[_levelSelectionDropdown.value]
            .LevelSections[_sectionSelectionDropdown.value]);
    }
}
