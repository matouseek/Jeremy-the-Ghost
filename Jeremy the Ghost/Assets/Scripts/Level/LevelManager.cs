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
    
    /// <summary>
    /// OnClick function for The Forest button in PlayMenu
    /// </summary>
    public void LoadTheForestLevel()
    {
        LoadLevel(Levels[2]);
    }
    
    private void LoadLevel(LevelDescription level)
    {
        CurrentlyPlayedLevel = level;
        SceneManager.LoadScene(level.Name);
    }
    
    /// <summary>
    /// Marks the currently played level as completed in the levels scriptable object.
    /// </summary>
    public static void CompleteLevel()
    {
        Instance.CurrentlyPlayedLevel.Completed = true;
    }
}
