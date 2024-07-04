using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that stores the data (description) of a level.
/// </summary>
[CreateAssetMenu(fileName = "NewLevelDescription", menuName = "Levels/LevelDescription")]
public class LevelDescription : ScriptableObject
{
    public string Name;
    [SerializeField] private bool _completed;

    public bool Completed
    {
        get => _completed;
        set
        {
            _completed = value;
            LevelCompletedAchievement.Completed = value;
        }
    }
    public List<LevelSectionDescription> LevelSections;
    public Achievement LevelCompletedAchievement;
}
