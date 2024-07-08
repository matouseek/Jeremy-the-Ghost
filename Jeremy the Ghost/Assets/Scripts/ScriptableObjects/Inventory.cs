using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Serves as the inventory of things the player has collected.
/// For now only via achievements.
/// </summary>
[CreateAssetMenu(fileName = "NewInventory", menuName = "Inventory")]
public class Inventory : DataPersistentScriptableObject
{
    // All eyes that the player collected
    // at index 0 is a null value resembling no eye sprite
    public List<Sprite> Eyes;
}
