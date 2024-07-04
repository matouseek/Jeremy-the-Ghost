using UnityEngine;

/// <summary>
/// Extends achievement class by adding a counter.
/// </summary>
[CreateAssetMenu(fileName = "NewCountingAchievement", menuName = "Achievements/CountingAchievement")]
public class CountingAchievement : Achievement
{
    [SerializeField] private int Count; // Current count

    public int CountToComplete; // Threshold value that is checked against when determining
                                // whether achievement is completed

    public void IncreaseCount()
    {
        if(Completed) return;
        if (++Count == CountToComplete) Completed = true;
    }
}
