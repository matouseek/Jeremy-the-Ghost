using UnityEngine;

public class MoveLogger : MonoBehaviour
{
    [SerializeField] private PSEnter _psEnter;
    [SerializeField] private LevelSectionDescription _levelSectionDescription;

    private void OnTriggerEnter2D(Collider2D other)
    {
        int movesUsed = _psEnter.MaxPsMoves - MoveManager.Instance.AvailableMoves;
        LeaderboardManager.SetEntry(_levelSectionDescription, movesUsed);
    }
}
