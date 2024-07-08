using UnityEngine;
using System.IO;
using System;

/// <summary>
/// Parent for ScriptableObjects that are used to store persistent data.
/// </summary>
public abstract class DataPersistentScriptableObject : ScriptableObject
{
    public virtual void SaveData()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, name);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string levelDescInJson = JsonUtility.ToJson(this, true);

            using (FileStream stream = new(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new(stream))
                {
                    writer.Write(levelDescInJson);
                }
            }
            
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
    public virtual void LoadData()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, name);
        if (!File.Exists(fullPath)) return;
        try
        {
            string dataToLoad;
            using (FileStream stream = new(fullPath, FileMode.Open))
            {
                using (StreamReader reader = new(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }
            
            JsonUtility.FromJsonOverwrite(dataToLoad, this);
            
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}
