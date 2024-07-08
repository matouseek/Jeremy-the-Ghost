using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that manages the saving and loading of data.
/// </summary>
public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager Instance { get; private set; }

    [SerializeField] private List<DataPersistentScriptableObject> dataPersistenceObjects;
    
    private void Awake() 
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
            DontDestroyOnLoad(gameObject);
        } 
    }

    private void Start()
    {
        LoadGame();
    }
    
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        foreach (var dataPersistentObject in dataPersistenceObjects)
        {
            dataPersistentObject.SaveData();
        }
    }

    public void LoadGame()
    {
        foreach (var dataPersistentObject in dataPersistenceObjects)
        {
            dataPersistentObject.LoadData();
        }
    }
        
}
