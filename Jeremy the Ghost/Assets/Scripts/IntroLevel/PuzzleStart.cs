using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleStart : MonoBehaviour
{
    [SerializeField] private GameObject noGoingBackCollider;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera puzzleCamera;
    [SerializeField] private int MaxPuzzleMoves = 20;
    
    private MoveController moveController;

    private void Start()
    {
        moveController = GameObject.Find("MoveController").GetComponent<MoveController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        puzzleCamera.Priority = 100;
        noGoingBackCollider.SetActive(true);
        moveController.SetMax(10);
        moveController.EnableMoveCounterTMP();
        gameObject.SetActive(false);
    }
}
