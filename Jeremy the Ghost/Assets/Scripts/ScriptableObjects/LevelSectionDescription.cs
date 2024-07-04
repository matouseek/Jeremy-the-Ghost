using UnityEngine;

/// <summary>
/// Class that stores the data (description) of
/// a levels platforming section.
/// </summary>
[CreateAssetMenu(fileName = "NewLevelSectionDescription", menuName = "Levels/LevelSectionDescription")]
public class LevelSectionDescription : ScriptableObject
{
    public string Name;
    public string LeaderboardPublicKey; // Used to access the leaderboard storing data about this section
}
