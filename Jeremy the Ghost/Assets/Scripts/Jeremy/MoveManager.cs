using UnityEngine;
using TMPro;

/// <summary>
/// A singleton class that manages the moves of Jeremy.
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
    
    // True -> The moves and Jeremy are reset after all moves used.
    // False -> Jeremy can move how many times he wants.
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

        // We return false if player used more moves than allowed (MaxMoves)
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
