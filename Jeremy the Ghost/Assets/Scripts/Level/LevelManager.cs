using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static readonly List<LevelDescription> Levels;
    private static LevelDescription CurrentlyPlayedLevel = null;
    
    
    /// <summary>
    /// OnClick function for Intro Level button in PlayMenu
    /// </summary>
    public void LoadIntroLevel()
    {
        LoadLevel(LevelManager.Levels[0]);
    }

    /// <summary>
    /// OnClick function for The Factory button in PlayMenu
    /// </summary>
    public void LoadTheFactoryLevel()
    {
        LoadLevel(LevelManager.Levels[1]);
    }
    
    private void LoadLevel(LevelDescription level)
    {
        LevelManager.CurrentlyPlayedLevel = level;
        SceneManager.LoadScene(level.Name);
    }
    
    /// <summary>
    /// Marks the next level in the game as playable to the player. This is done by setting
    /// a value 1 to the level name in player preferences.
    /// </summary>
    public static void CompleteLevel()
    {
        if (Levels.IndexOf(CurrentlyPlayedLevel) == Levels.Count - 1) return; // No new level will show after the last one
        CurrentlyPlayedLevel.Completed = true;
    }
}
