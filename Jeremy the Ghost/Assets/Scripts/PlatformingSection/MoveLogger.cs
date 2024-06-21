using UnityEngine;

public class MoveLogger : MonoBehaviour
{
    [SerializeField] private PSEnter _psEnter;
    [SerializeField] private LevelSectionDescription _levelSectionDescription;

    public void LogUsedMoves()
    {
        int movesUsed = _psEnter.MaxPsMoves - MoveManager.Instance.AvailableMoves;
        Debug.Log($"{_levelSectionDescription.Name} {movesUsed}");
        LeaderboardManager.SetEntry(_levelSectionDescription, movesUsed);
    }
}
