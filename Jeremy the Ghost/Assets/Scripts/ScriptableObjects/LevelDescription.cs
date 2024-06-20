using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelDescription", menuName = "LevelDescription")]
public class LevelDescription : ScriptableObject
{
    public string Name;
    public bool Completed;
    public List<LevelSectionDescription> LevelSections;
}
