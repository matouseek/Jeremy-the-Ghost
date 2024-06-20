using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private GameObject _nameInput;
    [SerializeField] private GameObject _leaderboard;
    [SerializeField] private TMP_Dropdown _levelSelection;
    [SerializeField] private TMP_Dropdown _sectionSelection;
    
    // All the columns of the leaderboard
    [SerializeField] private List<TextMeshProUGUI> _ranks;
    [SerializeField] private List<TextMeshProUGUI> _names;
    [SerializeField] private List<TextMeshProUGUI> _movesToFinish;
    [SerializeField] private List<TextMeshProUGUI> _dates;

    private void Start()
    {
       GetLeaderboard(LevelManager.Levels[0].LevelSections[0]);
    }

    /// <summary>
    /// Fetches the data of the wanted level section and fills the leaderboard with them. 
    /// </summary>
    public void GetLeaderboard(LevelSectionDescription sectionDescription)
    {
        LeaderboardCreator.GetLeaderboard(sectionDescription.LeaderboardPublicKey, (entries) =>
        {
            int loopLength = entries.Length < _names.Count ? entries.Length : _names.Count;
            for (int i = 0; i < loopLength; ++i)
            {
                _ranks[i].text = entries[i].Rank.ToString();
                _names[i].text = entries[i].Username;
                _movesToFinish[i].text = entries[i].Score.ToString();
                _dates[i].text = entries[i].Date.ToString();
            }
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
    }
    
    /// <summary>
    /// OnClick function for Submit button in Leaderboard
    /// </summary>
    public void SubmitName()
    {
        PlayerPrefs.SetString("Name", _nameInput.GetComponentInChildren<TMP_InputField>().text);
        _nameInput.SetActive(false);
        _leaderboard.SetActive(true);
    }
    
    /// <summary>
    /// OnClick function for Level selection dropdown in Leaderboard
    /// </summary>
    public void SelectLevel()
    {
        _sectionSelection.ClearOptions();
        foreach (var section in LevelManager.Levels[_levelSelection.value].LevelSections)
        {
            _sectionSelection.options.Add(new TMP_Dropdown.OptionData(section.Name));
        }

        _sectionSelection.RefreshShownValue();
    }
}
