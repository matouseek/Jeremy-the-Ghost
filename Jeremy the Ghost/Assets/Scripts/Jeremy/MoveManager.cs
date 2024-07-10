using UnityEngine;
using TMPro;

/// <summary>
/// A singleton class that manages the moves of Jeremy.
/// Handles the amount of moves Jeremy has available.
/// </summary>
public class MoveManager : MonoBehaviour
{
    public static MoveManager Instance { get; private set;  }

    [SerializeField] private TextMeshProUGUI _moveCounterTMP;
    private int _maxMoves;
    public int MaxMoves
    {
        private get => _maxMoves;
        set
        {
            _maxMoves = value;
            ResetMoves();
            UpdateMoveCounterTMP();
        }
    }
    private int _availableMoves;
    public int AvailableMoves => _availableMoves;
    
    // True -> The moves and Jeremy are reset after all moves used (When going through a platforming section)
    // False -> Jeremy can move how many times he wants (This is the case before or in between platforming sections)
    private bool _shouldRestrictAmountOfMoves;

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

    /// <returns> False if player used more moves than allowed and true otherwise.</returns>
    public bool DecreaseAvailableMoves()
    {
        if (!_shouldRestrictAmountOfMoves) return true;
        _availableMoves--;
        bool usedMoreThanMaxMoves = false;
        if (_availableMoves < 0)
        {
            _availableMoves = 0;
            usedMoreThanMaxMoves = true;
        }
        UpdateMoveCounterTMP();
        
        return !usedMoreThanMaxMoves;
    }

    public void ResetMoves()
    {
        _availableMoves = MaxMoves;
        UpdateMoveCounterTMP();
    }
    
    public void DisableMoveCounter()
    {
        _moveCounterTMP.enabled = false;
        _shouldRestrictAmountOfMoves = false;
    }
    
    public void EnableMoveCounter()
    {
        _moveCounterTMP.enabled = true;
        _shouldRestrictAmountOfMoves = true;
    }

    private void UpdateMoveCounterTMP()
    {
        _moveCounterTMP.text = $"{_availableMoves}/{MaxMoves} moves";
    }
}
