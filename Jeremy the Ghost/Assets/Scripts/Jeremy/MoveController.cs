using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveController : MonoBehaviour
{
    public static MoveController Instance { get; private set;  }

    [SerializeField] private TextMeshProUGUI moveCounterTMP;
    [SerializeField] private int maxMoves;
    [SerializeField] private int currentMoves;

    private bool _active;

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

    public bool DecreaseMoves()
    {
        if (!_active) return true;
        currentMoves--;
        bool returnVal = true;
        if (currentMoves < 0)
        {
            currentMoves = 0;
            returnVal = false;
        }
        UpdateMovesTMP();

        return returnVal;
    }

    public void ResetMoves()
    {
        currentMoves = maxMoves;
        UpdateMovesTMP();
    }

    public void SetMax(int newMax)
    {
        maxMoves = newMax;
        ResetMoves();
        UpdateMovesTMP();
    }

    public void DisableMoveCounter()
    {
        moveCounterTMP.enabled = false;
        _active = false;
    }
    
    public void EnableMoveCounter()
    {
        moveCounterTMP.enabled = true;
        _active = true;
    }

    private void UpdateMovesTMP()
    {
        moveCounterTMP.text = $"{currentMoves}/{maxMoves} moves";
    }
}
