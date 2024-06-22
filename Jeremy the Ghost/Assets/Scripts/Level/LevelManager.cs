using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set;  }

    [SerializeField] private List<LevelDescription> _levels;
    public List<LevelDescription> Levels => _levels;
    public LevelDescription CurrentlyPlayedLevel { get; private set; } = null;

    private void Awake() 
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    
    /// <summary>
    /// OnClick function for Intro Level button in PlayMenu
    /// </summary>
    public void LoadIntroLevel()
    {
        LoadLevel(Levels[0]);
    }

    /// <summary>
    /// OnClick function for The Factory button in PlayMenu
    /// </summary>
    public void LoadTheFactoryLevel()
    {
        LoadLevel(Levels[1]);
    }
    
    private void LoadLevel(LevelDescription level)
    {
        CurrentlyPlayedLevel = level;
        SceneManager.LoadScene(level.Name);
    }
    
    /// <summary>
    /// Marks the next level in the game as playable to the player. This is done by setting
    /// a value 1 to the level name in player preferences.
    /// </summary>
    public static void CompleteLevel()
    {
        if (Instance.Levels.IndexOf(Instance.CurrentlyPlayedLevel) == Instance.Levels.Count - 1) return; // No new level will show after the last one
        Instance.CurrentlyPlayedLevel.Completed = true;
    }
}
