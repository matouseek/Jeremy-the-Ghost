using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Achievements/Achievement")]
public class Achievement : DataPersistentScriptableObject
{
    public string Name;
    public string Description;
    public Sprite EyeRewardSprite; // Leaving this empty (null) signifies
                                // that there is no reward for completing
                                // this achievement
    [SerializeField] private Inventory _inventory;
    [SerializeField] private bool _completed; 
    public bool Completed
    {
        get => _completed;
        set
        {
            _completed = value;
            // Achievement is completed and there are eyes to be given to the player
            if (value && EyeRewardSprite != null)
            {
                _inventory.Eyes.Add(EyeRewardSprite);
            }
        }
    }
}
