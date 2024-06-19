using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _ranks;
    [SerializeField] private List<TextMeshProUGUI> _names;
    [SerializeField] private List<TextMeshProUGUI> _movesToFinish;
    [SerializeField] private List<TextMeshProUGUI> _dates;

    private static string _publicLeaderboardKey = "d7bc94c7f24c4a46d7714a141b60873d60a6ce42efd649e924f77757add0340d";

    private void Start()
    {
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(_publicLeaderboardKey, (entries) =>
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

    public static void SetEntry(string name, int movesToFinish)
    {
        LeaderboardCreator.UploadNewEntry(_publicLeaderboardKey, name, movesToFinish);
    }
}
