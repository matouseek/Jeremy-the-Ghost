using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleStart : MonoBehaviour
{
    [SerializeField] private GameObject noGoingBackCollider;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera puzzleCamera;
    private void OnTriggerEnter2D(Collider2D other)
    {
        puzzleCamera.Priority = 100;
        noGoingBackCollider.SetActive(true);
    }
}
