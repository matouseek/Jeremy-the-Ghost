using UnityEngine;

/// <summary>
/// Scriptable object that holds all the data related to Jeremy.
/// For now that is only customization related.
/// </summary>
[CreateAssetMenu(fileName = "NewJeremyDescription", menuName = "JeremyDescription")]
public class JeremyDescription : ScriptableObject
{
    public Color Color; // Can be set from the customization menu.
    public Sprite Eyes; // Are awarded from achievements
}
