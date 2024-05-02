using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveCounterTMP;
    [SerializeField] private int maxMoves;
    [SerializeField] private int currentMoves;

    public bool Active { get; private set; }

    public bool DecreaseMoves()
    {
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

    public void DisableMoveCounterTMP()
    {
        moveCounterTMP.enabled = false;
        Active = false;
    }
    
    public void EnableMoveCounterTMP()
    {
        moveCounterTMP.enabled = true;
        Active = true;
    }

    private void UpdateMovesTMP()
    {
        moveCounterTMP.text = $"{currentMoves}/{maxMoves} moves";
    }
}
